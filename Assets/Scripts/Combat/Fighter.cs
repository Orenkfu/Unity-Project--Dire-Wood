using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {
    [RequireComponent(typeof(Health))]
    public class Fighter : MonoBehaviour, IAction {
        Health target;
        [SerializeField] Transform hand;
        [SerializeField] Weapon defaultWeapon;
        private Weapon currentWeapon;
        Animator animator;
        Mover moveController;
        private float timeSinceLastAttack = 0f;
        void Awake() {
            animator = GetComponent<Animator>();
            moveController = GetComponent<Mover>();
        }
        void Start() {
            EquipWeapon(defaultWeapon);
            
        }
        public void Attack(GameObject target) {
            animator.ResetTrigger("stopAttack");
            GetComponent<ActionScheduler>().StartAction(this);
            setTarget(target.GetComponent<Health>());
        }

        public void EquipWeapon(Weapon weapon) {
            currentWeapon = weapon;
            weapon.Equip(hand, animator);
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
            return target != null && !target.GetComponent<Health>().isDead;
        }
        //An animation event!
        void Hit() {
            if (target == null) return;
            target.TakeDamage(currentWeapon.Damage);
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
    }
}
