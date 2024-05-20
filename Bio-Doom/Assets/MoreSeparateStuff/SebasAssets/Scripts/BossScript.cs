using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
        Attack,
    }

    private State state;

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 targetMovePosition;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] float targetRange, targetInCloseRange;

    private Vector3 startingPoint;

    EnemyNavMeshHandler enemyPathFinding;

    private Animator enemyAnimator;
    public float timeReleasAttack = 5f, rangeAttack, rotationSpeed =15;
    public Vector2 randomRange, waitRandomRange;
    private bool playerInRange = false, death;
    public Collider[] collArms;
    private float currentRandomAttack, timeToStop, currenRotaionSpeed;

    private void Awake()
    {
        state = State.Roaming;
    }

    private void Start()
    {
        enemyPathFinding = GetComponent<EnemyNavMeshHandler>();
        startingPoint = transform.position;
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Debug.Log("State Roaming");
                if (death) return;
                enemyPathFinding.RoamingMovement();
                enemyPathFinding.navMeshAgent.stoppingDistance = 0;
                enemyAnimator.SetTrigger("Walking");
                ColliderManager(false);
                FindTarget();

                break;

            case State.ChaseTarget:
                Debug.Log("State Chasing");
                if (death) return;
                enemyAnimator.SetTrigger("Running");
                enemyPathFinding.navMeshAgent.stoppingDistance = 0;
                enemyPathFinding.ChasingMethod(player.transform.position);
                playerInRange = Physics.CheckSphere(transform.position, targetInCloseRange, playerLayer);
                // Calcular la dirección hacia el jugador
                Vector3 directionToPlayer = player.transform.position - transform.position;
                directionToPlayer.y = 0; // Mantener la rotación en el plano horizontal

                // Calcular la rotación deseada hacia el jugador
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                // Interpolar suavemente hacia la rotación deseada
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                TargetCloseRange();
                OutOfRange();
                break;

            case State.Attack:
                Debug.Log("State Attack");
                AttackPlayer(); 
                enemyPathFinding.navMeshAgent.stoppingDistance = 5;
                playerInRange = Physics.CheckSphere(transform.position, targetInCloseRange, playerLayer);
                OutOfRange();

                break;
        }
    }

    private void ColliderManager(bool enabled)
    {
        foreach (Collider collider in collArms)
            collider.isTrigger = enabled;
    }

    public bool isAttackChosen = false; // Variable para controlar si el ataque ya fue elegido

    private void AttackPlayer()
    {
        if (timeToStop >= timeReleasAttack)
        {
            StartCoroutine(WaitAttack());
            isAttackChosen = false; // Reiniciar para el próximo ataque
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < targetInCloseRange)
            {
                // Calcular la dirección hacia el jugador
                Vector3 directionToPlayer = player.transform.position - transform.position;
                directionToPlayer.y = 0; // Mantener la rotación en el plano horizontal

                // Calcular la rotación deseada hacia el jugador
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                // Interpolar suavemente hacia la rotación deseada
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                ColliderManager(true);

                // Seleccionar el ataque solo una vez por ciclo de ataque
                if (!isAttackChosen)
                {
                    currentRandomAttack = Random.Range(randomRange.x, randomRange.y);
                    enemyAnimator.SetFloat("RandomAttack", currentRandomAttack);
                    isAttackChosen = true; // Marcar que el ataque ha sido elegido
                    currenRotaionSpeed = rotationSpeed;
                    // Configurar la animación de ataque basada en el ataque elegido
                    if (currentRandomAttack < 0.5f)
                    {
                        enemyAnimator.SetBool("Attack", true);
                        enemyAnimator.SetBool("Attack2", false);
                    }
                    else
                    {
                        enemyAnimator.SetBool("Attack", false);
                        enemyAnimator.SetBool("Attack2", true);
                    }
                }
                currenRotaionSpeed = rotationSpeed/2;
                enemyPathFinding.navMeshAgent.SetDestination(player.transform.position);
                timeToStop += Time.deltaTime;
            }
            else
            {
                ColliderManager(false);
                enemyAnimator.SetBool("Attack", false);
                enemyAnimator.SetBool("Attack2", false);
                timeToStop = 0f;
                isAttackChosen = false; // Reiniciar para el próximo ciclo de ataque
            }
        }
    }
    private IEnumerator WaitAttack()
    {
        death = true; 
        enemyAnimator.SetBool("Attack", false);
        enemyAnimator.SetBool("Attack2", false);
        enemyAnimator.SetBool("TimeToStop", death);
        yield return new WaitForSeconds(Random.Range(waitRandomRange.x, waitRandomRange.y));
        death = false;
        enemyAnimator.SetBool("TimeToStop", death);
        timeToStop = 0f;
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < targetRange)
            state = State.ChaseTarget;
    }

    private void TargetCloseRange()
    {

        if (playerInRange == true)
        {
            state = State.Attack;
        }
    }

    private void OutOfRange()
    {
        if (state == State.ChaseTarget)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > targetRange)
            {
                state = State.Roaming;
            }
        }
        else if (state == State.Attack)
        {
            if (!playerInRange)
            {
                state = State.ChaseTarget;
            }
        }
    }
    private void SetAttack(AnimationEvent animationEvent)
    {
        Debug.Log("hola");
        isAttackChosen = false;
    }
}
