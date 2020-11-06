using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkingForward_TestData
{
    public class ShortestPrefixCalculator
    {
		static readonly int MAX = 256;

		// Maximum length of an input word 
		static readonly int MAX_WORD_LEN = 500;

		// Trie Node. 
		public class TrieNode
		{
			public TrieNode[] child = new TrieNode[MAX];
			public int freq; // To store frequency 
			public TrieNode()
			{
				freq = 1;
				for (int i = 0; i < MAX; i++)
					child[i] = null;
			}
		}
		static TrieNode root;

		// Method to insert a new string into Trie 
		static void insert(String str)
		{
			// Length of the URL 
			int len = str.Length;
			TrieNode pCrawl = root;

			// Traversing over the length of given str. 
			for (int level = 0; level < len; level++)
			{
				// Get index of child node from 
				// current character in str. 
				int index = str[level];

				// Create a new child if not exist already 
				if (pCrawl.child[index] == null)
					pCrawl.child[index] = new TrieNode();
				else
					(pCrawl.child[index].freq)++;

				// Move to the child 
				pCrawl = pCrawl.child[index];
			}
		}

		static void findPrefixesUtil(TrieNode root, char[] prefix, int ind, List<string> res)
		{
			StringBuilder x = new StringBuilder();

			// Corner case 
			if (root == null)
				return;

			// Base case 
			if (root.freq == 1)
			{
				prefix[ind] = '\0';
				int i = 0;
				while (prefix[i] != '\0')
					x.Append(prefix[i++]);
				//Console.Write(prefix[i++]);
				//Console.Write(" ");
				res.Add(x.ToString());
				return;
			}

			for (int i = 0; i < MAX; i++)
			{
				if (root.child[i] != null)
				{
					prefix[ind] = (char)i;
					findPrefixesUtil(root.child[i], prefix, ind + 1, res);
				}
			}
		}

		public List<string> findPrefixes(List<string> geohashList, int n)
		{
			List<string> Result = new List<string>();

			root = new TrieNode();
			root.freq = 0;

			foreach (var geohash in geohashList)
			{
				insert(geohash);
			}

			char[] prefix = new char[MAX_WORD_LEN];

			findPrefixesUtil(root, prefix, 0, Result);

			return Result;
		}
	}
}
