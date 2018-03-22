prompt --application/shared_components/navigation/breadcrumbs/breadcrumb
begin
wwv_flow_api.create_menu(
 p_id=>wwv_flow_api.id(72820155995899208)
,p_name=>' Breadcrumb'
);
wwv_flow_api.create_menu_option(
 p_id=>wwv_flow_api.id(49508261236843969)
,p_parent_id=>wwv_flow_api.id(72823846230899220)
,p_short_name=>'Edit Patches'
,p_link=>'f?p=&APP_ID.:7:&SESSION.'
,p_page_id=>7
);
wwv_flow_api.create_menu_option(
 p_id=>wwv_flow_api.id(50535046246095340)
,p_parent_id=>wwv_flow_api.id(72820853868899212)
,p_short_name=>'Installed Patches - Dependency Order'
,p_link=>'f?p=&APP_ID.:5:&SESSION.::&DEBUG.:::'
,p_page_id=>5
);
wwv_flow_api.create_menu_option(
 p_id=>wwv_flow_api.id(50537043214111978)
,p_parent_id=>wwv_flow_api.id(72820853868899212)
,p_short_name=>'Unpromoted to &NEXT_LEVEL.'
,p_long_name=>'Patches Unpromoted'
,p_link=>'f?p=&APP_ID.:4:&SESSION.::&DEBUG.:::'
,p_page_id=>4
);
wwv_flow_api.create_menu_option(
 p_id=>wwv_flow_api.id(50539062396118273)
,p_parent_id=>wwv_flow_api.id(72820853868899212)
,p_short_name=>'Unapplied from &PREV_LEVEL.'
,p_long_name=>'Patches Unapplied'
,p_link=>'f?p=&APP_ID.:3:&SESSION.::&DEBUG.:::'
,p_page_id=>3
);
wwv_flow_api.create_menu_option(
 p_id=>wwv_flow_api.id(72820853868899212)
,p_parent_id=>0
,p_short_name=>'Patches'
,p_long_name=>'Home'
,p_link=>'f?p=&APP_ID.:1:&SESSION.::&DEBUG.:::'
,p_page_id=>1
);
wwv_flow_api.create_menu_option(
 p_id=>wwv_flow_api.id(72823846230899220)
,p_parent_id=>wwv_flow_api.id(72820853868899212)
,p_option_sequence=>20
,p_short_name=>'Installed in &PROMO_LEVEL.'
,p_long_name=>'Installed Patches'
,p_link=>'f?p=&APP_ID.:2:&SESSION.::&DEBUG.:::'
,p_page_id=>2
);
end;
/
