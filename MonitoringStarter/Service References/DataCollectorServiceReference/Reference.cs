﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MonitoringStarter.DataCollectorServiceReference {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServerStatus", Namespace="http://schemas.datacontract.org/2004/07/SharedLibrary")]
    public enum ServerStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Run = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Stop = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DataCollectorServiceReference.IDataCollectorService")]
    public interface IDataCollectorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataCollectorService/StartDataCollectorServer", ReplyAction="http://tempuri.org/IDataCollectorService/StartDataCollectorServerResponse")]
        bool StartDataCollectorServer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataCollectorService/StartDataCollectorServer", ReplyAction="http://tempuri.org/IDataCollectorService/StartDataCollectorServerResponse")]
        System.Threading.Tasks.Task<bool> StartDataCollectorServerAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataCollectorService/StopDataCollectorServer", ReplyAction="http://tempuri.org/IDataCollectorService/StopDataCollectorServerResponse")]
        bool StopDataCollectorServer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataCollectorService/StopDataCollectorServer", ReplyAction="http://tempuri.org/IDataCollectorService/StopDataCollectorServerResponse")]
        System.Threading.Tasks.Task<bool> StopDataCollectorServerAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataCollectorService/GetServerStatus", ReplyAction="http://tempuri.org/IDataCollectorService/GetServerStatusResponse")]
        MonitoringStarter.DataCollectorServiceReference.ServerStatus GetServerStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataCollectorService/GetServerStatus", ReplyAction="http://tempuri.org/IDataCollectorService/GetServerStatusResponse")]
        System.Threading.Tasks.Task<MonitoringStarter.DataCollectorServiceReference.ServerStatus> GetServerStatusAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataCollectorServiceChannel : MonitoringStarter.DataCollectorServiceReference.IDataCollectorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataCollectorServiceClient : System.ServiceModel.ClientBase<MonitoringStarter.DataCollectorServiceReference.IDataCollectorService>, MonitoringStarter.DataCollectorServiceReference.IDataCollectorService {
        
        public DataCollectorServiceClient() {
        }
        
        public DataCollectorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataCollectorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataCollectorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataCollectorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool StartDataCollectorServer() {
            return base.Channel.StartDataCollectorServer();
        }
        
        public System.Threading.Tasks.Task<bool> StartDataCollectorServerAsync() {
            return base.Channel.StartDataCollectorServerAsync();
        }
        
        public bool StopDataCollectorServer() {
            return base.Channel.StopDataCollectorServer();
        }
        
        public System.Threading.Tasks.Task<bool> StopDataCollectorServerAsync() {
            return base.Channel.StopDataCollectorServerAsync();
        }
        
        public MonitoringStarter.DataCollectorServiceReference.ServerStatus GetServerStatus() {
            return base.Channel.GetServerStatus();
        }
        
        public System.Threading.Tasks.Task<MonitoringStarter.DataCollectorServiceReference.ServerStatus> GetServerStatusAsync() {
            return base.Channel.GetServerStatusAsync();
        }
    }
}
