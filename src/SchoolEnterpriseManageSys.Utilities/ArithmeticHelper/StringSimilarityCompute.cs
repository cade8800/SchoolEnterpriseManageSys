using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.ArithmeticHelper
{
    ///<summary>
    ///Author: jary.zhang
    ///Desc: 计算两个字符串的相似度
    ///function 1:Levenshtein Distance
    ///function 2:Longest Common Subsquence, LCS
    ///function 3:KMP
    /// </summary>
    public class StringSimilarityCompute
    {
        /// <summary>
        /// 编辑距离（Levenshtein Distance）
        /// </summary>
        /// <param name="source">源串</param>
        /// <param name="target">目标串</param>
        /// <param name="similarity">输出：相似度，值在0～１</param>
        /// <returns>源串和目标串的相似度</returns>
        public static float LevenshteinDistance(String source, String target)
        {
            float similarity = 0f;

            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else if (String.IsNullOrEmpty(target))
            {
                return 0;
            }

            var score = new int[source.Length + 2, target.Length + 2];

            var INF = source.Length + target.Length;
            score[0, 0] = INF;
            for (var i = 0; i <= source.Length; i++)
            {
                score[i + 1, 1] = i;
                score[i + 1, 0] = INF;
            }
            for (var j = 0; j <= target.Length; j++)
            {
                score[1, j + 1] = j;
                score[0, j + 1] = INF;
            }

            var sd = new SortedDictionary<char, int>();
            foreach (var letter in (source + target))
            {
                if (!sd.ContainsKey(letter))
                    sd.Add(letter, 0);
            }

            for (var i = 1; i <= source.Length; i++)
            {
                var DB = 0;
                for (var j = 1; j <= target.Length; j++)
                {
                    var i1 = sd[target[j - 1]];
                    var j1 = DB;

                    if (source[i - 1] == target[j - 1])
                    {
                        score[i + 1, j + 1] = score[i, j];
                        DB = j;
                    }
                    else
                    {
                        score[i + 1, j + 1] = Math.Min(score[i, j], Math.Min(score[i + 1, j], score[i, j + 1])) + 1;
                    }

                    score[i + 1, j + 1] = Math.Min(score[i + 1, j + 1], score[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
                }

                sd[source[i - 1]] = i;
            }
            // 计算相似度
            Int32 MaxLength = Math.Max(source.Length + 1, target.Length + 1);   // 两字符串的最大长度
            similarity = ((float)(MaxLength - score[source.Length + 1, target.Length + 1])) / MaxLength;
            return similarity;
        }

        /// <summary>
        /// Lonest Common Subsequence LCS最长公共子序列算法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns>最长公共子序列的长度</returns>
        public static int LongestCommonSubsequence(string source, string target)
        {
            if (source.Length == 0 || target.Length == 0)
                return 0;
            var len = Math.Max(target.Length, source.Length);
            var subsequence = new int[len + 1, len + 1];
            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < target.Length; j++)
                {
                    if (source[i].Equals(target[j]))
                        subsequence[i + 1, j + 1] = subsequence[i, j] + 1;
                    else
                        subsequence[i + 1, j + 1] = 0;
                }
            }
            int maxSubquenceLenght = (from sq in subsequence.Cast<int>() select sq).Max<int>();
            return maxSubquenceLenght;
        }

        //function 3: KMP
    }
}
