using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GZTimeServer.Models;

namespace GZTimeServer.Models
{
    public static class Assets
    {
        /// <summary>
        /// 关卡和关卡识别码映射
        /// </summary>
        public static Dictionary<string, string> LevelCode = new Dictionary<string, string>
        {
            {"Start","Let's go"},//To Level 1
            {"A Letter", "16f42d83a7b573ba2b3f21a7fe0e1ca3"},//751100
            {"Memory", "cdcd267dd9829fbb9070142d231d16b0"},//immortal
            {"Anime", "d77eb394992936c977ce3f9a112f4b48"},//other condition
            {"Math", "e5221cdbfe0b4052f3192886042a161f" },//other condition
            {"Fans", "4c69b08054870e51a7c62009a4a41e81" },//lumodays
            {"Excel", "abe31b435a526399002c350bc19fa050" },//3Lhy0-EA
            {"Code", "c13367945d5d4c91047b3b50234aa7ab" },//other condition
            {"Billionaire", "0b9cb93dff91ce2d9e504a85e6fbac54" },//other condition
            {"Trees","db65c26e8fefa38dd77f5004b39eb589" },//TreesAreAlwaysThere.
            {"Emoji", "590a0531ddf3aaca3114dfbc0c62e086" },//Dreams_are_a1ways_be_with_you
            {"Maze", "d1f47e0b0089103812481452acb680f5" }, //other condition
            {"NO.12", "NOTHING" },//To Level 13
            {"Ending?", "67d52a0d523c34b0819a2358420731d6" },//not_nowthe world like a passer-by
            {"Ending.", "f342f928be56c2ab3e6d63e3eff9d677" }//End
        };

        /// <summary>
        /// 关卡与关卡序号映射
        /// </summary>
        public static Dictionary<string, int> LevelDic = new Dictionary<string, int>
        {
            { "Start", 0 },     // Done
            { "A Letter", 1},   // Done
            { "Memory", 2},     // Done
            { "Anime", 3 },     // Done
            { "Math", 4 },      // Done
            { "Fans", 5 },      // Done
            { "Excel", 6 },     // Done
            { "Code", 7 },      // Done
            { "Billionaire", 8 }, // Done
            { "Trees", 9 },     // Done
            { "Emoji", 10 },     // Done
            { "Maze", 11 },     // Done
            { "NO.12", 12 },    // Done
            { "Ending?", 13 },  // Done
            { "Ending.",14 }    // Done
        };

        /// <summary>
        /// 关卡序号与反馈信息预设
        /// </summary>
        public static Dictionary<int, string> LevelMsg = new Dictionary<int, string>
        {
            { 2, "<p>Next Level : ./Anime</p>"},
            { 3, "<p class=\"mb-0\">适度看番,不要变成肥宅哦!</p>\n" +
                "<p>Next Level: ./Math</p>" },
            { 4, "<p>Next Level : ./Unknown/friends</p>" },
            { 5, "<p class=\"mb-0\">要值得被那么多人记着.</p>\n" +
                "<p>Next Level: ./Unknown/what</p>" },
            { 6, "<p class=\"mb-0\">Well Done!</p>\n" +
                "<p>Next Level: ./Unknown/next</p>" },
            { 7, "<p class=\"mb-0\">Coding Codes!</p>\n" +
                "<p>Next Level: ./Youth/Dingtalk</p>" },
            { 8, "<p class=\"mb-0\">终于爆满了INT！!</p>\n" +
                "<p>Next Level: ./Trees</p>" },
            { 9, "<p class=\"mb-0\">To be your tree!</p>\n" +
                "<p>Next Level: ./Through/Time</p>" },
            { 10,"<p class=\"mb-0\">😏</p>\n" +
                "<p>Next Level: ./Maze</p>" },
            { 11, "<p class=\"mb-0\">你喜欢这个迷宫吗?</p>\n" +
                "<p>Next Level: ./NO12</p>" },
            { 13, "<p class=\"mb-0\">真正的结束.</p>\n" +
                "<p>Next Level: ./GZTime</p>" }
        };

        /// <summary>
        /// 番剧关卡的对应验证
        /// </summary>
        public static List<string> AnimeAns = new List<string>
        {
            "5-10:44","17-3:32","1-9:56","12-20:2"
        };

        /// <summary>
        /// 检验识别码与关卡是否匹配
        /// </summary>
        /// <param name="process">进度信息</param>
        /// <param name="code">识别码</param>
        public static bool CheckLevelProcess(LevelProcess process,string code) =>
            !string.IsNullOrWhiteSpace(process.Token) &&
            !string.IsNullOrWhiteSpace(process.LevelName) &&
            !string.IsNullOrWhiteSpace(process.IP) &&
            LevelCode.ContainsKey(process.LevelName) &&
            string.Equals(LevelCode[process.LevelName], code);

        /// <summary>
        /// 更新状态反馈
        /// </summary>
        public enum Status : byte
        {
            Success = 0,
            Fail = 1,
            NotFound = 2,
        }

        /// <summary>
        /// 生成随机16进制字符串
        /// </summary>
        /// <returns></returns>
        public static string GenRandomHEX() => Coding.MD5("GZTime:" + DateTime.Now.Ticks + new Random().Next());

        /// <summary>
        /// 生成原始编码Codekey
        /// </summary>
        /// <returns></returns>
        public static string GenOriginalCodeKey(string token) => Coding.MD5(token + "@" + DateTime.Now.Ticks);

        /// <summary>
        /// 生成随机编码字符串
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns></returns>
        public static string GenCodedString(string str)
        {
            Random random = new Random();
            string res = str;
            switch(random.Next(0,5))
            {
                case 0:
                    res = Coding.BASE64.Encode(res);
                    res = Coding.Reverse(res);
                    res = string.Join(' ',Coding.ToBase(Coding.ASCII(res), 16));
                    res = Coding.Reverse(res);
                    res = Coding.BASE64.Encode(res);
                    break;
                case 1:
                    res = Coding.BASE64.Encode(res);
                    res = Coding.Reverse(res);
                    res = string.Join(' ', Coding.ToBase(Coding.ASCII(res), 8));
                    res = Coding.Reverse(res);
                    res = Coding.BASE64.Encode(res);
                    break;
                case 2:
                    res = string.Join(' ', Coding.ToBase(Coding.ASCII(res), 16));
                    res = Coding.Reverse(res);
                    res = Coding.BASE64.Encode(res);
                    res = string.Join(' ', Coding.ASCII(res));
                    res = Coding.Reverse(res);
                    break;
                case 3:
                    res = string.Join(' ', Coding.ToBase(Coding.ASCII(res), 8));
                    res = Coding.BASE64.Encode(res);
                    res = Coding.Reverse(res);
                    res = Coding.BASE64.Encode(res);
                    res = string.Join(' ', Coding.ASCII(res));
                    break;
                case 4:
                    res = string.Join(' ', Coding.ASCII(res));
                    res = Coding.BASE64.Encode(res);
                    res = Coding.Reverse(res);
                    res = Coding.BASE64.Encode(res);
                    res = string.Join(' ', Coding.ToBase(Coding.ASCII(res), 2));
                    break;
            }
            return res;
        }
    }
}
