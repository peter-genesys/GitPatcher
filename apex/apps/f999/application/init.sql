set define off verify off feedback off
whenever sqlerror exit sql.sqlcode rollback
--------------------------------------------------------------------------------
--
-- ORACLE Application Express (APEX) export file
--
-- You should run the script connected to SQL*Plus as the Oracle user
-- APEX_180200 or as the owner (parsing schema) of the application.
--
-- NOTE: Calls to apex_application_install override the defaults below.
--
--------------------------------------------------------------------------------
begin
wwv_flow_api.import_begin (
 p_version_yyyy_mm_dd=>'2018.05.24'
,p_release=>'18.2.0.00.12'
,p_default_workspace_id=>10395162855923474
,p_default_application_id=>999
,p_default_owner=>'A171872B'
);
end;
/
 
prompt APPLICATION 999 - Tracker
--
-- Application Export:
--   Application:     999
--   Name:            Tracker
--   Exported By:     A171872B
--   Flashback:       0
--   Export Type:     Application Export
--   Version:         18.2.0.00.12
--   Instance ID:     248281793889903
--

-- Application Statistics:
--   Pages:                      8
--     Items:                   31
--     Processes:               10
--     Regions:                 15
--     Buttons:                  5
--   Shared Components:
--     Logic:
--       Items:                  3
--     Navigation:
--       Lists:                  2
--       Breadcrumbs:            1
--         Entries:              6
--       NavBar Entries:         1
--     Security:
--       Authentication:         1
--     User Interface:
--       Themes:                 1
--       Templates:
--         Page:                 9
--         Region:              13
--         Label:                5
--         List:                11
--         Popup LOV:            1
--         Calendar:             1
--         Breadcrumb:           1
--         Button:               3
--         Report:               9
--       LOVs:                   1
--     Globalization:
--     Reports:
--     E-Mail:
--   Supporting Objects:  Excluded

