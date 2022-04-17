﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Animations.Console_Menu
{
    public class PlayerScores
    {
        protected Dictionary<String, int> dictionary =
            new Dictionary<string, int>();


        public bool PlayerExists(String input)
        {
            return dictionary.ContainsKey(input);
        }

        public void AddPlayer(String name)
        {
            dictionary.Add(name, 0);
        }

        public void UpdateScore(String name, int score)
        {
            dictionary[name] = score;
        }

        public void SaveScores()
        {
            using (StreamWriter file = new StreamWriter("Assets/Animations/Console Menu/scores.txt"))
                foreach (var entry in dictionary)
                    file.WriteLine("{0}:{1}", entry.Key, entry.Value); 
        }

        public void PopulateDict()
        {
            var lines = File.ReadAllLines("Assets/Animations/Console Menu/scores.txt");
            for(var i = 0; i < lines.Length; i ++)
            {
                String[] pair = lines[i].Split(':');
                string name = pair[0];
                int score = Int32.Parse(pair[1]);
                if (!dictionary.ContainsKey(name))
                {
                    dictionary.Add(name, score);    
                }

                
            }
        }

        public List<String> getScoreList()
        {
           List<String> res = new List<string>();
            foreach(KeyValuePair<string, int> entry in dictionary)
            {
                res.Add("\t\t" + entry.Key + ":" + entry.Value);
                
            }
            return res;
        }
    }
}