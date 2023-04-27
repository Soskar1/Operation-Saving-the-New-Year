using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public IEnumerator LoadScene(int sceneBuildIndex)
    {
        animator.SetTrigger("Appear");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneBuildIndex);
    }
}
