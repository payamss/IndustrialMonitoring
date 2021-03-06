﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IndustrialMonitoring.Lib;
using SharedLibrary;

namespace IndustrialMonitoring
{
    /// <summary>
    /// Interaction logic for DialogSetTime.xaml
    /// </summary>
    public partial class DialogSetTime : Window
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public event EventHandler<TimeChangedEventArgs> TimeChanged;

        protected virtual void OnTimeChanged(TimeChangedEventArgs e)
        {
            EventHandler<TimeChangedEventArgs> handler = TimeChanged;
            if (handler != null) handler(this, e);
        }

        public DialogSetTime()
        {
            InitializeComponent();
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ApplyTime()
        {
            try
            {
                StartTime = (DateTime)DateTimePickerStartTime.SelectedValue;
                EndTime = (DateTime)DateTimePickerEndTime.SelectedValue;

                OnTimeChanged(new TimeChangedEventArgs(StartTime, EndTime));
            }
            catch (Exception ex)
            {
                Logger.LogIndustrialMonitoring(ex);
                
            }
        }

        private void DialogSetTime_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTimePickerStartTime.SelectedValue = StartTime;
                DateTimePickerEndTime.SelectedValue = EndTime;
            }
            catch (Exception ex)
            {
                Logger.LogIndustrialMonitoring(ex);
                
            }
        }

        private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {

                ApplyTime();
            }
            catch (Exception ex)
            {
                Logger.LogIndustrialMonitoring(ex);
                
            }
        }
    }
}
