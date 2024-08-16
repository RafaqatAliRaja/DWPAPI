using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Summary description for BusGenral
/// </summary>
public class BusReports
{
    public static string Employee;
    public static string EmpNos="";
    private DataSet _BusReports = new DataSet();

    private readonly DataBaseAccess access;

    public BusReports(IConfiguration configuration)
    {
        access = new DataBaseAccess(configuration);
    }

    public DataSet Reports
    {
        get
        {
            return _BusReports;
        }
        set
        {
            _BusReports = value;
        }
    }


    #region DWP

   

    public List<Dictionary<string, object>> GetCompaignSummary()
    {
        System.Collections.Hashtable h_Tbl = new System.Collections.Hashtable();


        if (_BusReports.Tables.Contains("tbl_CompaignSummary"))
            _BusReports.Tables.Remove("tbl_CompaignSummary");
        access.selectStoredProcedure(_BusReports, "PROC_DWP_GETCALLSUMMARY", h_Tbl, "tbl_CompaignSummary");

        return access.DT_to_DictionaryList(_BusReports.Tables["tbl_CompaignSummary"]);

    }

    public List<Dictionary<string, object>> GetDWPRealtimeMonitering()
    {
        System.Collections.Hashtable h_Tbl = new System.Collections.Hashtable();


        if (_BusReports.Tables.Contains("tbl_ReadtimeMonitereing"))
            _BusReports.Tables.Remove("tbl_ReadtimeMonitereing");
        access.selectStoredProcedure(_BusReports, "proc_dwp_getRealtimeMonitering", h_Tbl, "tbl_ReadtimeMonitereing");

        return access.DT_to_DictionaryList(_BusReports.Tables["tbl_ReadtimeMonitereing"]);

    }

    public List<Dictionary<string, object>> GetDWPAgentRealtimeStatus()
    {
        System.Collections.Hashtable h_Tbl = new System.Collections.Hashtable();

        
        if (_BusReports.Tables.Contains("tbl_AgentStatus"))
            _BusReports.Tables.Remove("tbl_AgentStatus");
        access.selectStoredProcedure(_BusReports, "proc_dwp_getAgentsRealtimeStatus", h_Tbl, "tbl_AgentStatus");

        return access.DT_to_DictionaryList(_BusReports.Tables["tbl_AgentStatus"]);

    }

    public List<Dictionary<string, object>> GetDWPAgentRealtimeAdherence()
    {
        System.Collections.Hashtable h_Tbl = new System.Collections.Hashtable();


        if (_BusReports.Tables.Contains("tbl_AgentAdherence"))
            _BusReports.Tables.Remove("tbl_CompaignSummary");
        access.selectStoredProcedure(_BusReports, "PROC_DWP_GETREALTIMEAGENTADHERENCE", h_Tbl, "tbl_AgentAdherence");

        return access.DT_to_DictionaryList(_BusReports.Tables["tbl_AgentAdherence"]);

    }

    public List<Dictionary<string, object>> GetDWPAgentWiseStats()
    {
        System.Collections.Hashtable h_Tbl = new System.Collections.Hashtable();


        if (_BusReports.Tables.Contains("tbl_AgentRealtimeStasts"))
            _BusReports.Tables.Remove("tbl_AgentRealtimeStasts");
        access.selectStoredProcedure(_BusReports, "PROC_DWP_GETREALTIMEAGENTSTATS", h_Tbl, "tbl_AgentRealtimeStasts");

        return access.DT_to_DictionaryList(_BusReports.Tables["tbl_AgentRealtimeStasts"]);

    }


    public List<Dictionary<string, object>> GetHourlyReport(string from, string to, string date)
    {
        try
        {
            Hashtable param = new Hashtable();
            param.Add("from", Convert.ToInt64(from));
            param.Add("to", Convert.ToInt64(to));
            param.Add("date", date);
            if (_BusReports.Tables.Contains("tblHourlyMoniter"))
                _BusReports.Tables.Remove("tblHourlyMoniter");
            access.selectStoredProcedure(_BusReports, "DWP_Get_HourlyReport", param, "tblHourlyMoniter");
            return access.DT_to_DictionaryList(_BusReports.Tables["tblHourlyMoniter"]);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public List<Dictionary<string, object>> GetIvRSelection(string from, string to, string date)
    {
        
        
        Hashtable param = new Hashtable();            
        param.Add("date", date);
        param.Add("from", Convert.ToInt64(from));
        param.Add("to", Convert.ToInt64(to));
        if (_BusReports.Tables.Contains("tblIvrSelection"))
            _BusReports.Tables.Remove("tblIvrSelection");
        access.selectStoredProcedure(_BusReports, "PROC_DWP_GETIVRSELECTION", param, "tblIvrSelection");
        return access.DT_to_DictionaryList(_BusReports.Tables["tblIvrSelection"]);
        
        
    }

    public List<Dictionary<string, object>> GetAGentPerformance(string date)
    {
        try
        {
            
            Hashtable param = new Hashtable();
            param.Add("date", date);
            if (_BusReports.Tables.Contains("tblAGetnPerforkance"))
                _BusReports.Tables.Remove("tblAGetnPerforkance");
            access.selectStoredProcedure(_BusReports, "PROC_DWP_GETAGENETPERFORMANCE", param, "tblAGetnPerforkance");
            return access.DT_to_DictionaryList(_BusReports.Tables["tblAGetnPerforkance"]);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

}