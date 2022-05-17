using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;
using System.Collections;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.Extensions;
using Nethereum.JsonRpc.UnityClient;

public class CallFunction : MonoBehaviour
{

    /*
    public partial class FunctionCallerDeployment : FunctionCallerDeploymentBase
    {
        public FunctionCallerDeployment() : base(BYTECODE) { }
        public FunctionCallerDeployment(string byteCode) : base(byteCode) { }
    }

    public class FunctionCallerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public FunctionCallerDeploymentBase() : base(BYTECODE) { }
        public FunctionCallerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CallNameFunction : CallNameFunctionBase { }

    [Function("callName", "string")]
    public class CallNameFunctionBase : FunctionMessage
    {
        [Parameter("address", "a", 1)]
        public virtual string A { get; set; }
    }

    public partial class CallNameOutputDTO : CallNameOutputDTOBase { }

    [FunctionOutput]
    public class CallNameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    // Use this for initialization
    void Start()
    {

        // StartCoroutine(DeployAndTransferToken());
    }


    //Sample of new features / requests
    public IEnumerator DeployAndTransferToken()
    {

        var url = "http://localhost:8545";
        var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
        var account = "0x12890d2cce102216644c59daE5baed380d84830c";
        //initialising the transaction request sender

        var transactionRequest = new TransactionSignedUnityRequest(url, privateKey);



       

        //Query request using our acccount and the contracts address (no parameters needed and default values)
        var queryRequest = new QueryUnityRequest<BalanceOfFunction, BalanceOfFunctionOutput>(url, account);
        yield return queryRequest.Query(new BalanceOfFunction() { Owner = account }, deploymentReceipt.ContractAddress);

        //Getting the dto response already decoded
        var dtoResult = queryRequest.Result;
        Debug.Log(dtoResult.Balance);

        var transactionTransferRequest = new TransactionSignedUnityRequest(url, privateKey);
        var newAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe";


        var transactionMessage = new TransferFunction
        {
            FromAddress = account,
            To = newAddress,
            Value = 1000,
        };

        yield return transactionTransferRequest.SignAndSendTransaction(transactionMessage, deploymentReceipt.ContractAddress);
        var transactionTransferHash = transactionTransferRequest.Result;

        Debug.Log("Transfer txn hash:" + transactionHash);

        transactionReceiptPolling = new TransactionReceiptPollingRequest(url);
        yield return transactionReceiptPolling.PollForReceipt(transactionTransferHash, 2);
        var transferReceipt = transactionReceiptPolling.Result;

        var transferEvent = transferReceipt.DecodeAllEvents<TransferEventDTO>();
        Debug.Log("Transferd amount from event: " + transferEvent[0].Event.Value);

        var getLogsRequest = new EthGetLogsUnityRequest(url);
        var eventTransfer = TransferEventDTO.GetEventABI();
        yield return getLogsRequest.SendRequest(eventTransfer.CreateFilterInput(deploymentReceipt.ContractAddress, account));

        var eventDecoded = getLogsRequest.Result.DecodeAllEvents<TransferEventDTO>();
        Debug.Log("Transferd amount from get logs event: " + eventDecoded[0].Event.Value);

    } */
}
