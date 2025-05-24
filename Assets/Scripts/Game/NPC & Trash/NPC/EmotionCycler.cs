//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EmotionCycler : MonoBehaviour
//{
//    private AIBehaviour ai;
//    private string[] emotions = { "happy", "sad", "wondering" };
//    private int currentIndex = 0;

//    private void Start()
//    {
//        StartCoroutine(CycleEmotion());
//    }

//    private IEnumerator CycleEmotion()
//    {
//        while (true)
//        {
//            int randomIndex = Random.Range(0, emotions.Length);
//            ai.emotion = emotions[randomIndex];
//            yield return new WaitForSeconds(10f);
//        }
//    }
//}

