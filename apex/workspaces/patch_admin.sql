set define off
set verify off
set feedback off
WHENEVER SQLERROR EXIT SQL.SQLCODE ROLLBACK
begin wwv_flow.g_import_in_progress := true; end;
/
 
--       AAAA       PPPPP   EEEEEE  XX      XX
--      AA  AA      PP  PP  EE       XX    XX
--     AA    AA     PP  PP  EE        XX  XX
--    AAAAAAAAAA    PPPPP   EEEE       XXXX
--   AA        AA   PP      EE        XX  XX
--  AA          AA  PP      EE       XX    XX
--  AA          AA  PP      EEEEEE  XX      XX
begin
select value into wwv_flow_api.g_nls_numeric_chars from nls_session_parameters where parameter='NLS_NUMERIC_CHARACTERS';
execute immediate 'alter session set nls_numeric_characters=''.,''';
end;
/
prompt  WORKSPACE 26496728947047737
--
-- Workspace, User Group, User, and Team Development Export:
--   Date and Time:   17:51 Friday November 7, 2014
--   Exported By:     ADMIN
--   Export Type:     Workspace Export
--   Version:         4.2.6.00.03
--   Instance ID:     69403566450056
--
-- Import:
--   Using Instance Administration / Manage Workspaces
--   or
--   Using SQL*Plus as the Oracle user APEX_040200
 
begin
    wwv_flow_api.set_security_group_id(p_security_group_id=>26496728947047737);
end;
/
----------------
-- W O R K S P A C E
-- Creating a workspace will not create database schemas or objects.
-- This API creates only the meta data for this APEX workspace
prompt  Creating workspace PATCH_ADMIN...
begin
wwv_flow_api.g_varchar2_table := wwv_flow_api.empty_varchar2_table;
end;
/
begin
wwv_flow_fnd_user_api.create_company (
  p_id => 26496827186047766
 ,p_provisioning_company_id => 26496728947047737
 ,p_short_name => 'PATCH_ADMIN'
 ,p_display_name => 'PATCH_ADMIN'
 ,p_first_schema_provisioned => 'PATCH_ADMIN'
 ,p_company_schemas => 'PATCH_ADMIN'
 ,p_account_status => 'ASSIGNED'
 ,p_allow_plsql_editing => 'Y'
 ,p_allow_app_building_yn => 'Y'
 ,p_allow_sql_workshop_yn => 'Y'
 ,p_allow_websheet_dev_yn => 'Y'
 ,p_allow_team_development_yn => 'Y'
 ,p_allow_to_be_purged_yn => 'Y'
 ,p_allow_restful_services_yn => 'Y'
 ,p_source_identifier => 'PATCH_AD'
 ,p_path_prefix => 'PATCH_ADMIN'
 ,p_workspace_image => wwv_flow_api.g_varchar2_table
);
end;
/
----------------
-- G R O U P S
--
prompt  Creating Groups...
begin
wwv_flow_api.create_user_groups (
  p_id => 2608207072028100,
  p_GROUP_NAME => 'OAuth2 Client Developer',
  p_SECURITY_GROUP_ID => 10,
  p_GROUP_DESC => 'Users authorized to register OAuth2 Client Applications');
end;
/
begin
wwv_flow_api.create_user_groups (
  p_id => 2608113138028088,
  p_GROUP_NAME => 'RESTful Services',
  p_SECURITY_GROUP_ID => 10,
  p_GROUP_DESC => 'Users authorized to use RESTful Services with this workspace');
end;
/
begin
wwv_flow_api.create_user_groups (
  p_id => 3095603269454247,
  p_GROUP_NAME => 'SQL Developer',
  p_SECURITY_GROUP_ID => 10,
  p_GROUP_DESC => 'Users authorized to use SQL Developer with this workspace');
end;
/
----------------
-- U S E R S
-- User repository for use with APEX cookie-based authentication.
--
prompt  Creating Users...
begin
wwv_flow_fnd_user_api.create_fnd_user (
  p_user_id      => '26496617540047737',
  p_user_name    => 'ADMIN',
  p_first_name   => 'Peter',
  p_last_name    => 'Burgess',
  p_description  => '',
  p_email_address=> 'peter.a.burgess@gmail.com',
  p_web_password => '5758E9DA307EE5ADC724B2EF33D50EBE',
  p_web_password_format => 'HEX_ENCODED_DIGEST_V2',
  p_group_ids                    => '',
  p_developer_privs              => 'ADMIN:CREATE:DATA_LOADER:EDIT:HELP:MONITOR:SQL',
  p_default_schema               => 'PATCH_ADMIN',
  p_account_locked               => 'N',
  p_account_expiry               => to_date('201411071750','YYYYMMDDHH24MI'),
  p_failed_access_attempts       => 0,
  p_change_password_on_first_use => 'Y',
  p_first_password_use_occurred  => 'N',
  p_allow_app_building_yn        => 'Y',
  p_allow_sql_workshop_yn        => 'Y',
  p_allow_websheet_dev_yn        => 'Y',
  p_allow_team_development_yn    => 'Y',
  p_allow_access_to_schemas      => '');
end;
/
----------------
--Application Builder Preferences
--
----------------
--Click Count Logs
--
----------------
--csv data loading
--
----------------
--mail
--
----------------
--mail log
--
----------------
--app models
--
----------------
--password history
--
begin
  wwv_flow_api.create_password_history (
    p_id => 26496931651047770,
    p_user_id => 26496617540047737,
    p_password => 'C26D4FA0C84393634AEC96ADE4930E74');
end;
/
----------------
--preferences
--
----------------
--service mods
--
----------------
--query builder
--
----------------
--sql scripts
--
----------------
--sql commands
--
----------------
--user access log
--
 
prompt ...RESTful Services
 
-- SET SCHEMA
 
begin
 
   wwv_flow_api.g_id_offset := 0;
   wwv_flow_hint.g_schema   := 'PATCH_ADMIN';
   wwv_flow_hint.check_schema_privs;
 
end;
/

 
--------------------------------------------------------------------
prompt  SCHEMA PATCH_ADMIN - User Interface Defaults, Table Defaults
--
-- Import using sqlplus as the Oracle user: APEX_040200
-- Exported 17:51 Friday November 7, 2014 by: ADMIN
--
 
--------------------------------------------------------------------
prompt User Interface Defaults, Attribute Dictionary
--
-- Exported 17:51 Friday November 7, 2014 by: ADMIN
--
-- SHOW EXPORTING WORKSPACE
 
begin
 
   wwv_flow_api.g_id_offset := 0;
   wwv_flow_hint.g_exp_workspace := 'PATCH_ADMIN';
 
end;
/

commit;
begin
execute immediate 'begin sys.dbms_session.set_nls( param => ''NLS_NUMERIC_CHARACTERS'', value => '''''''' || replace(wwv_flow_api.g_nls_numeric_chars,'''''''','''''''''''') || ''''''''); end;';
end;
/
set verify on
set feedback on
set define on
prompt  ...done
