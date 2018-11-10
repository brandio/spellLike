using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class DialogueLoader : MonoBehaviour {
    public string fullPath;

    public struct Conversation
    {
        public string text;
        public int id;
        public int side;
        public int[] childrenIds;
        public int shake;
    }

    Dictionary<int, Conversation> conversations = new Dictionary<int, Conversation>();
	
    void Awake()
    {
        LoadAllConversations();
    }

	void LoadAllConversations () {
        string line;

        // Read the file and display it line by line.
        System.IO.StreamReader file = new System.IO.StreamReader(fullPath);
        while ((line = file.ReadLine()) != null)
        {

            Conversation conversation = JsonUtility.FromJson<Conversation>(line);
            conversations.Add(conversation.id, conversation);
            //conversations[] = conversation;
        }

        file.Close();
    }

    public List<Conversation> GetConversations(int[] ids)
    {
        List<Conversation> convos = new List<Conversation>();
        foreach (int id in ids)
        {
            convos.Add(conversations[id]);
        }
        return convos;
    }
}
