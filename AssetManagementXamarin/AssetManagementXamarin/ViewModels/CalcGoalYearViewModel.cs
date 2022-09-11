using AssetManagementXamarin.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AssetManagementXamarin.ViewModels
{
    public class CalcGoalYearViewModel : BaseViewModel
    {
        private static int _thisYear = DateTime.Now.Year;

        private ICommand _buttonClickCommand;
        public ICommand ButtonClickCommand => _buttonClickCommand ?? (_buttonClickCommand = new RelayCommand(OnButtonClick));

        #region props

        private IEnumerable<ResultModel> _gridData = new List<ResultModel>();
        public IEnumerable<ResultModel> GridData
        {
            get { return _gridData; }
            set { SetProperty(ref _gridData, value); }
        }

        private int _startYear = _thisYear;
        public int StartYear
        {
            get { return _startYear; }
            set { SetProperty(ref _startYear, value); }
        }

        private int _startAsset;
        public int StartAsset
        {
            get { return _startAsset; }
            set { SetProperty(ref _startAsset, value); }
        }

        private int _goalDividend;
        public int GoalDividend
        {
            get { return _goalDividend; }
            set { SetProperty(ref _goalDividend, value); }
        }

        private int _expectedDividendRate;
        public int ExpectedDividendRate
        {
            get { return _expectedDividendRate; }
            set { SetProperty(ref _expectedDividendRate, value); }
        }

        private int _anualAffordableCash;
        public int AnualAffordableCash
        {
            get { return _anualAffordableCash; }
            set { SetProperty(ref _anualAffordableCash, value); }
        }

        private bool _isCompound;
        public bool IsCompound
        {
            get { return _isCompound; }
            set { SetProperty(ref _isCompound, value); }
        }

        private bool _hasErrorGoalDividend;
        public bool HasErrorGoalDividend
        {
            get { return _hasErrorGoalDividend; }
            set { SetProperty(ref _hasErrorGoalDividend, value); }
        }

        private bool _hasErrorExpectedDividendRate;
        public bool HasErrorExpectedDividendRate
        {
            get { return _hasErrorExpectedDividendRate; }
            set { SetProperty(ref _hasErrorExpectedDividendRate, value); }
        }

        private bool _hasErrorAnualAffordableCash;
        public bool HasErrorAnualAffordableCash
        {
            get { return _hasErrorAnualAffordableCash; }
            set { SetProperty(ref _hasErrorAnualAffordableCash, value); }
        }

        private bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
            set { SetProperty(ref _hasError, value); }
        }

        private int _resultValue;
        public int ResultValue
        {
            get { return _resultValue; }
            set { SetProperty(ref _resultValue, value); }
        }

        private bool _resultVisibility;
        public bool ResultVisibility
        {
            get { return _resultVisibility; }
            set { SetProperty(ref _resultVisibility, value); }
        }

        #endregion

        public CalcGoalYearViewModel()
        {
            Title = "簡易計算";
        }

        private void OnButtonClick()
        {
            HasError = !Validate();
            if (HasError)
            {
                ResultValue = 0;
                ResultVisibility = false;
                GridData = null;
                return;
            }

            CreateGridData();
            ResultVisibility = true;
        }

        private bool Validate()
        {
            HasErrorGoalDividend = GoalDividend <= 0;
            HasErrorExpectedDividendRate = ExpectedDividendRate <= 0;
            return !HasErrorGoalDividend && !HasErrorExpectedDividendRate;
        }

        private void CreateGridData()
        {
            int currentAsset = StartAsset;
            var data = new List<ResultModel>();

            var counter = 0;
            while(true)
            {
                var year = StartYear + counter;
                currentAsset += AnualAffordableCash;
                var expectedDividend = currentAsset * ExpectedDividendRate / 100;

                data.Add(new ResultModel(year, currentAsset, expectedDividend));

                if (expectedDividend > GoalDividend)
                {
                    GridData = data;
                    ResultValue = year;
                    return;
                }

                if (IsCompound)
                    currentAsset += expectedDividend;

                counter++;
            }
        }
    }
}