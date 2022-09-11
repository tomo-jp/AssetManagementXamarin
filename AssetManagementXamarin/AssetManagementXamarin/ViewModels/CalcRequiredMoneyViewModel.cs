using AssetManagementXamarin.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AssetManagementXamarin.ViewModels
{
    public class CalcRequiredMoneyViewModel : BaseViewModel
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

        private int _goalRetireYear = _thisYear;
        public int GoalRetireYear
        {
            get { return _goalRetireYear; }
            set { SetProperty(ref _goalRetireYear, value); }
        }

        private int _expectedDividendRate;
        public int ExpectedDividendRate
        {
            get { return _expectedDividendRate; }
            set { SetProperty(ref _expectedDividendRate, value); }
        }

        private bool _isCompound;
        public bool IsCompound
        {
            get { return _isCompound; }
            set { SetProperty(ref _isCompound, value); }
        }

        private bool _hasErrorGoalRetireYear;
        public bool HasErrorGoalRetireYear
        {
            get { return _hasErrorGoalRetireYear; }
            set { SetProperty(ref _hasErrorGoalRetireYear, value); }
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

        public CalcRequiredMoneyViewModel()
        {
            Title = "簡易計算";
        }

        private void OnButtonClick()
        {
            var timePeriod = GoalRetireYear - StartYear + 1;

            HasError = !Validate(timePeriod);
            if (HasError)
            {
                ResultValue = 0;
                ResultVisibility = false;
                GridData = null;
                return;
            }

            var requiredAmount = GoalDividend * 100 / ExpectedDividendRate - StartAsset;
            ResultValue = requiredAmount <= 0 ? 0 : requiredAmount / timePeriod;
            CreateGridData(timePeriod);
            ResultVisibility = true;
        }

        private bool Validate(int timePeriod)
        {
            HasErrorGoalRetireYear = timePeriod <= 0;
            HasErrorGoalDividend = GoalDividend <= 0;
            HasErrorExpectedDividendRate = ExpectedDividendRate <= 0;
            return !HasErrorGoalRetireYear && !HasErrorGoalDividend && !HasErrorExpectedDividendRate;
        }

        private void CreateGridData(int timePeriod)
        {
            int currentAsset = StartAsset;
            var data = new List<ResultModel>();
            for (int i = 0; i < timePeriod; i++)
            {
                var year = StartYear + i;
                currentAsset += ResultValue;

               data.Add(new ResultModel(year, currentAsset, currentAsset * ExpectedDividendRate / 100));
            }
            GridData = data;
        }
    }
}