prompt --application/shared_components/navigation/lists/navigation_menu
begin
wwv_flow_api.create_list(
 p_id=>wwv_flow_api.id(90164838464218934)
,p_name=>'Navigation Menu'
,p_list_status=>'PUBLIC'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(90164951914218935)
,p_list_item_display_sequence=>1
,p_list_item_link_text=>'Patches'
,p_list_item_link_target=>'f?p=&APP_ID.:1:&SESSION.::&DEBUG.::::'
,p_list_item_current_type=>'COLON_DELIMITED_PAGE_LIST'
,p_list_item_current_for_pages=>'1,2,3,4,5'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(90202223960231221)
,p_list_item_display_sequence=>11
,p_list_item_link_text=>'Installed in &PROMO_LEVEL.'
,p_list_item_link_target=>'f?p=&APP_ID.:2:&SESSION.::&DEBUG.::::'
,p_list_item_current_type=>'TARGET_PAGE'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(90202534127232759)
,p_list_item_display_sequence=>21
,p_list_item_link_text=>'Unapplied from &PREV_LEVEL.'
,p_list_item_link_target=>'f?p=&APP_ID.:3:&SESSION.::&DEBUG.::::'
,p_list_item_disp_cond_type=>'ITEM_IS_NOT_NULL'
,p_list_item_disp_condition=>'PREV_LEVEL'
,p_list_item_current_type=>'TARGET_PAGE'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(90202871747233800)
,p_list_item_display_sequence=>31
,p_list_item_link_text=>'Unpromoted to &NEXT_LEVEL.'
,p_list_item_link_target=>'f?p=&APP_ID.:4:&SESSION.::&DEBUG.::::'
,p_list_item_disp_cond_type=>'ITEM_IS_NOT_NULL'
,p_list_item_disp_condition=>'NEXT_LEVEL'
,p_list_item_current_type=>'TARGET_PAGE'
);
wwv_flow_api.create_list_item(
 p_id=>wwv_flow_api.id(90203148289235621)
,p_list_item_display_sequence=>41
,p_list_item_link_text=>'Dependencies'
,p_list_item_link_target=>'f?p=&APP_ID.:5:&SESSION.::&DEBUG.::::'
,p_list_item_current_type=>'TARGET_PAGE'
);
end;
/
