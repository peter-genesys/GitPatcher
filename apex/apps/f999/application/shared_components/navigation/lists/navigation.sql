prompt --application/shared_components/navigation/lists/navigation
begin
wwv_flow_api.create_list(
 p_id=>wwv_flow_api.id(72820549044899211)
,p_name=>'Navigation'
,p_list_status=>'PUBLIC'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(72820670118899212)
,p_list_item_display_sequence=>10
,p_list_item_link_text=>'Installed Patches'
,p_list_item_link_target=>'f?p=&APP_ID.:2:&APP_SESSION.::&DEBUG.:'
,p_list_item_current_type=>'COLON_DELIMITED_PAGE_LIST'
,p_list_item_current_for_pages=>'2'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(46326068548619851)
,p_list_item_display_sequence=>20
,p_list_item_link_text=>'Patches Unapplied'
,p_list_item_link_target=>'f?p=&APP_ID.:3:&SESSION.::&DEBUG.::::'
,p_list_item_current_type=>'TARGET_PAGE'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(46331764149139933)
,p_list_item_display_sequence=>30
,p_list_item_link_text=>'Patches Unpromoted'
,p_list_item_link_target=>'f?p=&APP_ID.:4:&SESSION.::&DEBUG.::::'
,p_list_item_current_type=>'TARGET_PAGE'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(48532547636781392)
,p_list_item_display_sequence=>40
,p_list_item_link_text=>'Installed Patches - Dependency Order'
,p_list_item_link_target=>'f?p=&APP_ID.:5:&SESSION.::&DEBUG.::::'
,p_list_item_current_type=>'TARGET_PAGE'
);
end;
/
