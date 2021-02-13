using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HDV
{
    public class Debug : UnityEngine.Debug
    {
        public new static void Log(object msg)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.Log(msg);
#endif
        }

        public new static void LogWarning(object msg)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogWarning(msg);
#endif
        }

        public new static void LogError(object msg)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogError(msg);
            Break();
#endif
        }

        public new static void Assert(bool condition)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Assertions.Assert.IsTrue(condition);
#endif
        }

        public static void AssertIsNotNull<T>(T obj) where T : class
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            try
            {
                Assert(obj != null);
            }
            catch
            {
                LogError(obj);
            }
#endif
            //TODO: 빌드버전은 에러 띄어주고 어플을 안전하게 종료하고 로그를 서버로 보내야
        }

        public static void AssertContainKey<T, S>(IDictionary<T, S> dic, T key)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            try
            {
                Assert(dic.ContainsKey(key));
            }
            catch
            {
                LogError(dic + " " + key);
            }
#endif
            //TODO: 빌드버전은 에러 띄어주고 어플을 안전하게 종료하고 로그를 서버로 보내야
        }

        public static void AssertNotContainKey<T, S>(IDictionary<T, S> dic, T key)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            try
            {
                Assert(!dic.ContainsKey(key));
            }
            catch
            {
                LogError(dic + " " + key);
            }
#endif
            //TODO: 빌드버전은 에러 띄어주고 어플을 안전하게 종료하고 로그를 서버로 보내야
        }

        public static void AssertIsNotEmpty(string str)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            try
            {
                Assert(!string.IsNullOrEmpty(str));
            }
            catch
            {
                LogError("string is empty!");
            }
#endif
            //TODO: 빌드버전은 에러 띄어주고 어플을 안전하게 종료하고 로그를 서버로 보내야
        }

    }
}
