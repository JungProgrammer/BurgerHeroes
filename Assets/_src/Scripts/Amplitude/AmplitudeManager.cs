using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Analytics
{
    public class AmplitudeManager : Singleton<AmplitudeManager>
    {
        // events variables
        private const string LevelWinEvent = "level_win";
        private const string MainMenuLevelEvent = "main_menu";
        private const string GameStartEvent = "game_start";
        private const string LevelStartEvent = "level_start";


        // user params
        private const string RegistrationDate = "reg_day";
        private const string SessionId = "session_id";
        private const string DaysAfter = "days_after";
        private const string CurrentSoft = "current_soft";
        private const string LevelLast = "level_last";


        void Awake () {
            Amplitude amplitude = Amplitude.Instance;
            amplitude.logging = true;
            amplitude.init("e29ac4190e4eefa46c32ba0160118cdd");


            // Events
            SendGameStartEvent();


            // Properties
            SetOnceRegistrationDate();
            SetNumberOfSession();
            SetCountOfDaysAfterRegistration();
        }
        

        public void SendTestEvent()
        {
            Amplitude.Instance.logEvent("EVENT_NAME_HERE");
        }


        public void SendLevelWinEvent()
        {
            Amplitude.Instance.logEvent(LevelWinEvent);
        }

        public void SendMenuLevelEvent()
        {
            Amplitude.Instance.logEvent(MainMenuLevelEvent);
        }

        public void SendGameStartEvent()
        {
            Amplitude.Instance.logEvent(GameStartEvent);
        }

        public void SendLevelStartEvent()
        {
            Amplitude.Instance.logEvent(LevelStartEvent);
        }


        public void SetOnceRegistrationDate()
        {
            string registrationDate = PlayerPrefs.GetString("RegistrationDate", "");

            DateTime currentDate = DateTime.Now;


            if (registrationDate == "")
                PlayerPrefs.SetString("RegistrationDate", currentDate.ToString());

            string currentDateString = DateTime.Now.ToString("dd/MM/yy");

            Amplitude.Instance.setOnceUserProperty(RegistrationDate, currentDateString);
        }

        public void SetNumberOfSession()
        {
            int currentSessionId = PlayerPrefs.GetInt("SessionId", 1);

            Amplitude.Instance.setUserProperty(SessionId, currentSessionId);

            PlayerPrefs.SetInt("SessionId", currentSessionId + 1);
        }

        public void SetCountOfDaysAfterRegistration()
        {
            string registrationDateString = PlayerPrefs.GetString("RegistrationDate", "");

            if (registrationDateString == "")
            {
                Amplitude.Instance.setUserProperty(DaysAfter, 0);
                return;
            }

            DateTime registrationDate = DateTime.Parse(registrationDateString).Date;
            DateTime currentDate = DateTime.Now.Date;

            int countOfDaysAfterRegistration = (int)(currentDate - registrationDate).TotalDays;

            Amplitude.Instance.setUserProperty(DaysAfter, countOfDaysAfterRegistration);
        }

        public void SetCurrentSoft()
        {
            int coinsCount = PlayerPrefs.GetInt("CoinsCount", 0);

            Amplitude.Instance.setUserProperty(CurrentSoft, coinsCount);
        }

        public void SetLastLevel()
        {
            int lastLevel = PlayerPrefs.GetInt("LevelNumber", 1);

            Amplitude.Instance.setUserProperty(LevelLast, lastLevel);
        }
    }   
}
