--application/shared_components/navigation/breadcrumbs
prompt  ...breadcrumbs
--
 
begin
 
wwv_flow_api.create_menu (
  p_id=> 40766016689551138 + wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_name=> ' Breadcrumb');
 
wwv_flow_api.create_menu_option (
  p_id=>17454121930495899 + wwv_flow_api.g_id_offset,
  p_menu_id=>40766016689551138 + wwv_flow_api.g_id_offset,
  p_parent_id=>40769706924551150 + wwv_flow_api.g_id_offset,
  p_option_sequence=>10,
  p_short_name=>'Edit Patches',
  p_long_name=>'',
  p_link=>'f?p=&APP_ID.:7:&SESSION.',
  p_page_id=>7,
  p_also_current_for_pages=> '');
 
wwv_flow_api.create_menu_option (
  p_id=>40766714562551142 + wwv_flow_api.g_id_offset,
  p_menu_id=>40766016689551138 + wwv_flow_api.g_id_offset,
  p_parent_id=>0,
  p_option_sequence=>10,
  p_short_name=>'Home',
  p_long_name=>'',
  p_link=>'f?p=&APP_ID.:1:&APP_SESSION.::&DEBUG.',
  p_page_id=>1,
  p_also_current_for_pages=> '');
 
wwv_flow_api.create_menu_option (
  p_id=>40769706924551150 + wwv_flow_api.g_id_offset,
  p_menu_id=>40766016689551138 + wwv_flow_api.g_id_offset,
  p_parent_id=>40766714562551142 + wwv_flow_api.g_id_offset,
  p_option_sequence=>20,
  p_short_name=>'Installed Patches',
  p_long_name=>'',
  p_link=>'f?p=&APP_ID.:2:&APP_SESSION.::&DEBUG.',
  p_page_id=>2,
  p_also_current_for_pages=> '');
 
null;
 
end;
/

prompt  ...page templates for application: 123
--
