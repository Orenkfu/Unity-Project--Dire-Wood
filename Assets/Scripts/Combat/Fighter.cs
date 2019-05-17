using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System.Collections.Generic;
using RPG.Saving;

namespace RPG.Combat {
    [RequireComponent(typeof(Health))]
    public class Fighter : MonoBehaviour, IAction, ISaveable {
        Health target;
        [SerializeField] Transform rightHand;
        [SerializeField] Transform leftHand;
        [SerializeField] Weapon defaultWeapon;
       // [SerializeField] Arrow arrows;
        Quiver quiver;
        private Weapon currentWeapon;
        Animator animator;
        Mover moveController;
        GameObject weaponGameobject;

        private float timeSinceLastAttack = 0f;
        void Awake() {
            animator = GetComponent<Animator>();
            quiver = GetComponent<Quiver>();
            moveController = GetComponent<Mover>();
        }
        void Start() {
            if (!currentWeapon) {
            EquipWeapon(defaultWeapon);
            }

        }
        public void Attack(GameObject target) {
            animator.ResetTrigger("stopAttack");
            GetComponent<ActionScheduler>().StartAction(this);
            setTarget(target.GetComponent<Health>());
        }

        public void EquipWeapon(Weapon weapon) {
            if (weaponGameobject != null) {
                Destroy(weaponGameobject);
            }
            if (!weapon) {
                print(gameObject + "has no weapon..?");
                weapon = defaultWeapon;
            }
            currentWeapon = weapon;
            weaponGameobject = weapon.Equip(rightHand, leftHand, animator);
        }


        void Update() {
            timeSinceLastAttack += Time.deltaTime;
            if (!CanAttack(target)) return;
            bool isInWeaponRange = Vector3.Distance(transform.position, target.transform.position) < currentWeapon.range;
            if (!isInWeaponRange) {
                moveController.MoveTo(target.transform.position, 1f);
            } else if (timeSinceLastAttack >= currentWeapon.attackSpeed) {
                AttackBehaviour();
            }
        }
        void AttackBehaviour() {
            animator.SetTrigger("attack");
            moveController.Cancel();
            timeSinceLastAttack = 0f;
            transform.LookAt(target.transform);
        }
        public bool CanAttack(GameObject target) {
            return target != null && !target.GetComponent<Health>().isDead;
        }
        public bool CanAttack(Health target) {
            return target != null && !target.isDead;
        }
        //An animation event! triggered on melee attacks
        void Hit() {
            if (target == null) return;
            target.TakeDamage(currentWeapon.Damage);
        }

        //An animation event! triggered on range attacks
        void Shoot() {
            if (target == null || quiver == null ||  !quiver.CanUse()) {
                Cancel();
                print("Shoot animation triggered but no arrow shot. Target: " + target);
                return;
            }
            var launchArrow = quiver.Use();
            launchArrow.transform.position = leftHand.position;
            var bodyMass = target.GetComponent<CharacterBodyCenter>();
            Vector3 targetPosition = bodyMass ? bodyMass.bodyCenter.transform.position : target.transform.position;
            launchArrow.Launch(targetPosition, currentWeapon.Damage + quiver.Damage, gameObject.tag);
        }

        public void Cancel() {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
            moveController.Cancel();
            setTarget(null);
        }

        public void setTarget(Health target) {
            this.target = target;
        }

        public void RestoreState(object state) {
           currentWeapon = (Weapon)FindObjectOfType<WeaponStorage>().Get((string)state);
            EquipWeapon(currentWeapon);
        }

        public object CaptureState() {
            if (!currentWeapon) {
                print(gameObject + " is a fighter but has no current weapon.");
                return "";
            }
            return currentWeapon.name;
        }
    }
}
