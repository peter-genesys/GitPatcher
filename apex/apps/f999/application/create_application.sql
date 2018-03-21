prompt --application/create_application
begin
wwv_flow_api.create_flow(
 p_id=>wwv_flow.g_flow_id
,p_display_id=>nvl(wwv_flow_application_install.get_application_id,999)
,p_owner=>nvl(wwv_flow_application_install.get_schema,'A171872B')
,p_name=>nvl(wwv_flow_application_install.get_application_name,'Tracker')
,p_alias=>nvl(wwv_flow_application_install.get_application_alias,'TRACKER_DEV')
,p_page_view_logging=>'YES'
,p_page_protection_enabled_y_n=>'Y'
,p_checksum_salt=>'C52C49899492BE3DDEDA0F12325F7F3C3A4DF5A4AB9EE1C605F04E44FD3EBA66'
,p_bookmark_checksum_function=>'MD5'
,p_compatibility_mode=>'4.2'
,p_flow_language=>'en'
,p_flow_language_derived_from=>'FLOW_PRIMARY_LANGUAGE'
,p_direction_right_to_left=>'N'
,p_flow_image_prefix => nvl(wwv_flow_application_install.get_image_prefix,'')
,p_authentication=>'PLUGIN'
,p_authentication_id=>wwv_flow_api.id(72819053363899202)
,p_application_tab_set=>0
,p_logo_image=>'TEXT:Tracker'
,p_proxy_server=> nvl(wwv_flow_application_install.get_proxy,'')
,p_flow_version=>'release 1.0'
,p_flow_status=>'AVAILABLE_W_EDIT_LINK'
,p_flow_unavailable_text=>'This application is currently unavailable at this time.'
,p_exact_substitutions_only=>'Y'
,p_browser_cache=>'N'
,p_browser_frame=>'D'
,p_deep_linking=>'Y'
,p_runtime_api_usage=>'T:O:W'
,p_rejoin_existing_sessions=>'P'
,p_csv_encoding=>'Y'
,p_substitution_string_01=>'ALIAS_DEV'
,p_substitution_value_01=>'TRACKER_DEV'
,p_substitution_string_02=>'ALIAS_TEST'
,p_substitution_value_02=>'TRACKER_TEST'
,p_substitution_string_03=>'ALIAS_PROD'
,p_substitution_value_03=>'TRACKER_PROD'
,p_substitution_string_04=>'APP_ID_DEV'
,p_substitution_value_04=>'999'
,p_substitution_string_05=>'APP_ID_TEST'
,p_substitution_value_05=>'997'
,p_substitution_string_06=>'APP_ID_PROD'
,p_substitution_value_06=>'998'
,p_last_updated_by=>'PETER'
,p_last_upd_yyyymmddhh24miss=>'20180321214804'
,p_file_prefix => nvl(wwv_flow_application_install.get_static_app_file_prefix,'')
,p_ui_type_name => null
);
end;
/
