using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 54, fileName = "QuestSpeaker", menuName = "MyData/QuestSpeaker")]
public class QuestSpeakersData : ScriptableObject
{
    public List<string> _firstTalk;
    public List<string> _defaultTalk;
    public List<string> _questCompleteTalk;
}
