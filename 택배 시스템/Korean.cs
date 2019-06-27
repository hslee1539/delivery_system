using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 택배_시스템
{
    public class Korean
    {
        const int BASE_CODE = 0xAC00;
        const int BASE_CONSONANT = 0x1100;
        const int BASE_VOWEL = 0x1161;
        /// <summary>
        /// 숫자 키보드를 초성 순서에 맞게 매칭한 값
        /// </summary>
        string[] initialSound = { "4", "444", "5", "6", "666", "55", "00", "7", "777", "8", "888", "0", "9", "999", "99", "44", "66", "77", "88" };
        /// <summary>
        /// 숫자 키보드를 중성 순서에 맞게 매칭한 값
        /// </summary>
        string[] medialSound = { "12", "121", "122", "1221", "21", "211", "221", "2211", "23", "2312", "23121", "231","223", "32", "3221", "32211", "321","322", "3", "13", "1"};
        /// <summary>
        /// 숫자 키보드를 종성 순서에 맞게 매칭한 값
        /// </summary>
        string[] finalSound = { string.Empty, "4", "444", "48", "5", "59", "588", "6", "55", "554", "5500", "557", "558", "5566", "5577", "5588", "00", "7", "78", "8", "888", "0", "9", "99", "44", "66", "77", "88" };

        public Korean()
        {

        }
        /// <summary>
        /// 소스에서 타깃의 위치를 반환합니다.
        /// </summary>
        /// <param name="source">문자열의 배열로, target이 포함되면 0을 포함한 자연수가 나옵니다.</param>
        /// <param name="target">문자열로, 소스에서 이 객체와 같은 값을 가지는 지점을 찾습니다.</param>
        /// <returns>찾은 위치가 반환힙니다. 없을 시 -1을 반환합니다.</returns>
        public int IndexOf(string[] source, string target)
        {
            for(int i = 0, max = source.Length; i < max; i++)
            {
                if (source[i].Equals(target))
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(string[] source, string target, int targetStart, int targetCount)
        {
            for(int i = 0, j = 0, max = source.Length; i < max; i++)
            {
                if(source[i].Length == targetCount)
                {
                    for (j = 0; j < targetCount && source[i][j] == target[j + targetStart]; j++) ;
                    if(j == targetCount)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public char MakeSingleConsonant(int indexOf)
        {
            byte[] bytes = BitConverter.GetBytes((short)(BASE_CONSONANT + indexOf));
            return Char.Parse(Encoding.Unicode.GetString(bytes));
        }
        public char MakeSingleVowel(int indexOf)
        {
            byte[] bytes = BitConverter.GetBytes((short)(BASE_VOWEL + indexOf));
            return Char.Parse(Encoding.Unicode.GetString(bytes));
        }
        public char MakeCompleteChar(int indexOfInitial, int indexOfMedial, int indexOfFinal)
        {
            int tempFinalConsonant = 0;
            if (indexOfFinal > 0)
            {
                tempFinalConsonant = indexOfFinal;
            }

            int jungcnt = medialSound.Length;
            int jongcnt = finalSound.Length;
            int completeChar = indexOfInitial * jungcnt * jongcnt + indexOfMedial * jongcnt + tempFinalConsonant + BASE_CODE;
            byte[] naeBytes = BitConverter.GetBytes((short)(completeChar));
            return Char.Parse(Encoding.Unicode.GetString(naeBytes));
        }
        public bool IsConsonant(char number)
        {
            return '0' == number || ('3' < number || number < '9' + 1);
        }
        public bool IsVowel(char number)
        {
            return '0' < number && number < '4';
        }
        public string Parse( string number)
        {
            int process = 0;
            //number에서 처리할 배열의 시작 위치
            int start = 0;
            int extraStart = 0;
            //number에서 start변수에서 부터 count수를 더한 범위 내에 처리.
            int count = 0;
            //반환 값. process가 끝나면 문자 하나하나가 쌓임.
            string returnValue = "";
            // 0 : current, 1: pre
            int[] indexOfInitial = { -1, -1 };
            int[] indexOfMedial = { -1, -1 };
            int[] indexOfFinal = { -1, -1 };
            int[] IndexOfChar = new int[5];

            for (int i = 0, max = number.Length - 1; i < max; i++)
            {
                count++;
                indexOfInitial[1] = this.IndexOf(this.initialSound, number, start, count + 1);
                indexOfMedial[1] = this.IndexOf(this.medialSound, number, start, count + 1);
                indexOfFinal[1] = this.IndexOf(this.finalSound, number, extraStart, count + start - extraStart + 1);
                indexOfInitial[0] = this.IndexOf(this.initialSound, number, start , count);
                indexOfMedial[0] = this.IndexOf(this.medialSound, number, start, count);
                indexOfFinal[0] = this.IndexOf(this.finalSound, number, extraStart, count + start - extraStart);
                L2:  switch (process)
                {
                    
                    case 0:// 첫 단계
                        //첫 단계에서 모음이 홀로 오다가 완성 되는 경우.
                        //문자를 확정하고, 처리하고 단계를 유지.
                        if(indexOfMedial[0] > -1 && indexOfMedial[1] == -1)
                        {
                            returnValue += this.MakeSingleVowel(indexOfMedial[1]);
                            extraStart = start = i + 1;
                            count = 0;
                        }
                        //첫 단계에서 자음이 완성되는 경우.
                        //다음 단계로 이동.
                        else if(indexOfInitial[0] > -1 && indexOfInitial[1] == -1)
                        {
                            IndexOfChar[process] = indexOfInitial[1];
                            process++;
                            extraStart = start = i;
                            count = 0;
                        }
                        //특수 문자의 처리.
                        //무조건 첫단계로 이동.
                        if ('a' < number[i] - 1)
                        {
                            extraStart = start = i + 1;
                            count = 0;
                            process = 0;
                            if(indexOfInitial[0] > -1)
                            {
                                returnValue += this.MakeSingleConsonant(indexOfInitial[0]);
                            }
                            else if(indexOfMedial[0] > -1)
                            {
                                returnValue += this.MakeSingleVowel(indexOfMedial[0]);
                            }

                            switch (number[i])
                            {
                                case 'a':
                                    returnValue += '.';
                                    break;
                                case 'b':
                                    returnValue += ' ';
                                    break;
                            }
                        }
                        break;
                    case 1://두 번째 단계
                        //두번째 단계에서 자음이 입력되는 경우.(완성이 아닌 입력 주의!)
                        //이전 문자를 확정하고 이전 단계로 이동.
                        if (indexOfInitial[0] > -1)
                        {
                            process = 0;
                            returnValue += this.MakeSingleConsonant(IndexOfChar[process]);
                            goto L2;
                        }
                        //두번째 단계에서 모음이 완성되는 경우.(자음 + 모음 상황)
                        //다음 단계로 이동.
                        else if(indexOfMedial[0] > -1 && indexOfMedial[1] == -1)
                        {
                            IndexOfChar[process] = indexOfMedial[1];
                            process++;
                            extraStart = start = i + 1;
                            count = 0;
                        }
                        //특수 문자의 처리.
                        //무조건 첫단계로 이동.
                        if ('a' < number[i] - 1)
                        {
                            extraStart = start = i + 1;
                            count = 0;
                            process = 0;
                            if (indexOfMedial[0] > -1)
                            {
                                returnValue += this.MakeCompleteChar(IndexOfChar[0], IndexOfChar[1], 0);
                            }
                        }
                        break;
                    case 2://세 버째 단계
                        //세번째 단계에서 모음이 입력되는 경우.(완성이 아닌 입력에 주의)
                        //이전 자음 + 모음을 확정하고, -2단계(처음 단계)로 이동.
                        if (indexOfMedial[0] > -1)
                        {
                            process = 0;
                            returnValue += this.MakeCompleteChar(IndexOfChar[process], IndexOfChar[process + 1], 0);
                        }
                        //세번째 단계에서 자음(받침)이 완성되는 경우.
                        //다음 단계로 이동.
                        else if (indexOfFinal[0] == -1 && indexOfFinal[1] > -1)
                        {
                            //자음이 두개인 받침.
                            IndexOfChar[process] = indexOfFinal[1];
                            //받침에서 오른쪽에 해당되는 자음
                            IndexOfChar[process + 2] = indexOfInitial[1];
                            process++;
                            extraStart = start = i;
                            count = 0;                            
                        }
                        //세번째 단계에서 자음이 완성되는 경우. (else if 가 아님!)
                        //단계 유지. (괋 + ㅗ = 괄호 처리를 위한 조건문.)
                        if (indexOfInitial[0] == -1 && indexOfInitial[1] > -1)
                        {
                            start = i;
                            count = 0;
                            //밫임에서 왼쪽에 해당되는 자음.
                            IndexOfChar[process + 1] = indexOfInitial[1];                           
                        }
                        //특수 문자의 처리.
                        //무조건 첫단계로 이동.
                        if ('a' < number[i] - 1)
                        {
                            extraStart = start = i + 1;
                            count = 0;
                            process = 0;
                           if(indexOfFinal[1] > -1)
                            {
                                returnValue += this.MakeCompleteChar(IndexOfChar[0], IndexOfChar[1], IndexOfChar[2]);
                            }
                        }
                        break;
                    case 3://마지막 단계
                           //마지막 단계에서 자음이 완성되는 경우.
                           //이전 자음 + 모음 + 자음을 확정하고, -2단계로 이동.
                        if (indexOfInitial[0] == -1 && indexOfInitial[1] > -1)
                        {
                            process = 1; // process -= 2;
                            returnValue += this.MakeCompleteChar(IndexOfChar[0], IndexOfChar[1], IndexOfChar[2]);
                            IndexOfChar[0] = indexOfInitial[1];
                            extraStart = start = i;
                            count = 0;
                        }
                        //마지막 단계에서 모음이 완성되는 경우.(곡 + ㅣ = 고기)
                        //이전 자음 + 모음 + 왼쪽 자음을 확정하고, 세번째 단계에서 오른쪽 자음 + 모음을 처리 할 수 있게 만들고, 세번째 단계로 이동.
                        else if (indexOfMedial[0] == -1 && indexOfMedial[1] > -1)
                        {
                            process = 2; // process -= 1;
                            returnValue += this.MakeCompleteChar(IndexOfChar[0], IndexOfChar[1], IndexOfChar[3]);
                            IndexOfChar[0] = IndexOfChar[4];
                            IndexOfChar[1] = indexOfMedial[1];
                            extraStart = start = i;
                            count = 0;
                        }
                        //특수 문자의 처리.
                        //무조건 첫단계로 이동.
                        if ('a' < number[i] - 1)
                        {
                            extraStart = start = i + 1;
                            count = 0;
                            process = 0;
                            if (indexOfInitial[1] > -1)
                            {
                                returnValue += this.MakeSingleConsonant(IndexOfChar[0]);
                            }
                            else if(indexOfMedial[1]> -1)
                            {
                                returnValue += this.MakeCompleteChar(IndexOfChar[0], IndexOfChar[1], 0);
                            }
                        }
                        break;
                }
            }
            return returnValue;
        }
    }
    public static class KoreanCharMaker
    {
        //한글 코드 값 = 초성*중성개수*종성 개수+중성 *종성개수+종성+BASE 코드(0xAC00)
        const int BASE_CODE = 0xAC00;//BASE 코드
        const int BASE_INIT = 0x1100; //'ㄱ' 
        const int BASE_VOWEL = 0x1161; //'ㅏ'        
        static string chostring = "rRseEfaqQtTdwWczxvg";//초성에 사용하는 키 목록
        static string chostring_k = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";//한글 초성
        static string[] chostrs = null;
        static string[] chostrs_k = { "ㄱ", "ㄲ", "ㄴ", "ㄷ", "ㄸ", "ㄹ", "ㅁ", "ㅂ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅉ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ"};

        static string[] jungstrs = null;//중성에 사용하는 문자열 목록
        static string[] jungstrs_k = null;//한글 중성 
        static string[] jungstrs_c = null;//천지인용 중성

        static string[] jongstrs = null;//종성에 사용하는 문자열 목록
        static string[] jongstrs_k = null;//한글 종성

        static KoreanCharMaker()
        {
            jungstrs = new string[] { "k", "o", "i", "O", "j", "p", "u", "P", "h", "hk", "ho", "hl", "y", "n", "nj", "np", "nl", "b", "m", "ml", "l" };
            jungstrs_k = new string[] { "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ", "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ" };
            jongstrs = new string[] { string.Empty, "r", "R", "rt", "s", "sw", "sg", "e", "f", "fr", "fa", "fq", "ft", "fx", "fv", "fg", "a", "q", "qt", "t", "T", "d", "w", "c", "z", "x", "v", "g" };
            jongstrs_k = new string[] { string.Empty, "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };
        }

        public static int GetInitSoundCode(char ch)
        {
            int index = chostring_k.IndexOf(ch);
            if (index != -1)
            {
                return index;
            }
            return chostring.IndexOf(ch);
        }
        public static int GetVowelCode(string str)
        {
            int cnt = jungstrs.Length;
            for (int i = 0; i < cnt; i++)
            {
                if (jungstrs[i] == str)
                {
                    return i;
                }
            }
            for (int i = 0; i < cnt; i++)
            {
                if (jungstrs_k[i] == str)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int GetVowelCode(char ch)
        {
            return GetVowelCode(ch.ToString());
        }
        public static int GetFinalConsonantCode(string str)
        {
            int cnt = jongstrs.Length;
            for (int i = 0; i < cnt; i++)
            {
                if (jongstrs[i] == str)
                {
                    return i;
                }
            }
            for (int i = 0; i < cnt; i++)
            {
                if (jongstrs_k[i] == str)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int GetFinalConsonantCode(char ch)
        {
            return GetFinalConsonantCode(ch.ToString());
        }
        public static char GetSingleJa(int value)
        {
            byte[] bytes = BitConverter.GetBytes((short)(BASE_INIT + value));
            return Char.Parse(Encoding.Unicode.GetString(bytes));
        }
        public static char GetSingleVowel(int value)
        {
            byte[] bytes = BitConverter.GetBytes((short)(BASE_VOWEL + value));
            return Char.Parse(Encoding.Unicode.GetString(bytes));
        }
        public static char GetCompleteChar(int init_sound, int vowel, int final)
        {
            int tempFinalConsonant = 0;
            if (final >= 0)
            {
                tempFinalConsonant = final;
            }

            int jungcnt = jungstrs.Length;
            int jongcnt = jongstrs.Length;
            int completeChar = init_sound * jungcnt * jongcnt + vowel * jongcnt + tempFinalConsonant + BASE_CODE;
            byte[] naeBytes = BitConverter.GetBytes((short)(completeChar));
            return Char.Parse(Encoding.Unicode.GetString(naeBytes));
        }
    }
}
