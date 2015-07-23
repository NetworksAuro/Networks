using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTOS.DataLayer
{
    public class BoxofficeOvrr
    {

                public string bo_override {get ; set;} 		
	            // Subscription
                public decimal	bo_sub_ff {get ; set;}    			   			
                public decimal	bo_sub_tax1 {get ; set;}		
                public decimal	bo_sub_net_comm  {get ; set;}   		
                public decimal	bo_sub_tax_ff_comm {get ; set;}	
	            // Phone
                public decimal	bo_ph_ff   {get ; set;}	   						
                public decimal	bo_ph_tax1  {get ; set;}				
                public decimal	bo_ph_net_comm  {get ; set;}	    		
                public decimal	bo_ph_tax_ff_comm  {get ; set;}	
	            // Internet
                public decimal	bo_web_ff   {get ; set;}	  						
                public decimal	bo_web_tax1 {get ; set;}				
                public decimal	bo_web_net_comm  {get ; set;}	   		
                public decimal	bo_web_tax_ff_comm	{get ; set;}	
	            // Credit Card
                public decimal	bo_cc_ff     	{get ; set;}					
                public decimal	bo_cc_tax1 		{get ; set;}	
                public decimal	bo_cc_net_comm  {get ; set;}   		
                public decimal	bo_cc_tax_ff_comm	{get ; set;}	
                // Remote/Outlet
                public decimal	bo_outlet_ff     {get ; set;}						
                public decimal	bo_outlet_tax1 		{get ; set;}
                public decimal	bo_outlet_net_comm  {get ; set;}    	
                public decimal	bo_outlet_tax_ff_comm	{get ; set;}
                // Single Ticket
                public decimal	bo_single_tix_ff    {get ; set;} 					
                public decimal	bo_single_tix_tax1 {get ; set;}		
                public decimal	bo_single_tix_net_comm  {get ; set;}   	
                public decimal	bo_single_tax_ff_comm	{get ; set;}
                public decimal	bo_small_group_ff     	{get ; set;}			
	            // Group 1
                public decimal	bo_small_group_tax1 	{get ; set;}	
                public decimal	bo_small_group_net_comm  {get ; set;}   
                public decimal	bo_small_tax_ff_comm	{get ; set;}
                public decimal	bo_large_group_ff     	{get ; set;}				
                // Group 2
                public decimal	bo_large_group_tax1 	{get ; set;}	
                public decimal	bo_large_group_net_comm	 {get ; set;}
                public decimal	bo_large_tax_ff_comm	 {get ; set;}
                public decimal	bo_large_tax_ff_tot_comm	{get ; set;}	
		        //Other fields
                public decimal	bo_other_per_ff	{get ; set;}				
                public decimal	bo_other_per_tax1 {get ; set;}		
                public decimal	bo_other_per_net_comm	{get ; set;}
                public decimal	bo_other_usd_ff		{get ; set;}
                public decimal	bo_other_usd_tax1	{get ; set;}	
                public decimal	bo_other_usd_net_comm	{get ; set;}
                public int	bo_other3_t_sold		{get ; set;}
                public decimal	bo_other3_gross_rcpt {get ; set;}		
                public decimal	bo_other3_ff			{get ; set;}
                public decimal	bo_other3_tax1		{get ; set;}
                public decimal	bo_other3_net_comm		{get ; set;}
                public int	bo_other4_t_sold		{get ; set;}
                public decimal	bo_other4_gross_rcpt	{get ; set;}	
                public decimal	bo_other4_ff			{get ; set;}
                public decimal	bo_other4_tax1		{get ; set;}
                public decimal	bo_other4_net_comm		{get ; set;}
                public int	bo_other5_t_sold		  {get ; set;}
                public decimal	bo_other5_gross_rcpt	{get ; set;}	
                public decimal	bo_other5_ff			{get ; set;}
                public decimal	bo_other5_tax1		{get ; set;}
                public decimal bo_other5_net_comm { get; set; }

    }
}