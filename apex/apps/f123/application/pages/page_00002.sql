--application/pages/page_00002
prompt  ...PAGE 2: Installed Patches
--
 
begin
 
wwv_flow_api.create_page (
  p_flow_id => wwv_flow.g_flow_id
 ,p_id => 2
 ,p_user_interface_id => 40764710612551125 + wwv_flow_api.g_id_offset
 ,p_tab_set => 'TS1'
 ,p_name => 'Installed Patches'
 ,p_step_title => 'Installed Patches'
 ,p_step_sub_title => 'Installed Patches'
 ,p_step_sub_title_type => 'TEXT_WITH_SUBSTITUTIONS'
 ,p_include_apex_css_js_yn => 'Y'
 ,p_autocomplete_on_off => 'ON'
 ,p_page_is_public_y_n => 'N'
 ,p_cache_page_yn => 'N'
 ,p_help_text => 
'No help is available for this page.'
 ,p_last_updated_by => 'PETER'
 ,p_last_upd_yyyymmddhh24miss => '20141107180349'
  );
null;
 
end;
/

declare
  s varchar2(32767) := null;
  l_clob clob;
  l_length number := 1;
begin
s:=s||'select PATCHES_V.PATCH_NAME as PATCH_NAME,'||unistr('\000a')||
'    PATCHES_V.DB_SCHEMA as DB_SCHEMA,'||unistr('\000a')||
'    PATCHES_V.BRANCH_NAME as BRANCH_NAME,'||unistr('\000a')||
'    PATCHES_V.TAG_FROM as TAG_FROM,'||unistr('\000a')||
'    PATCHES_V.TAG_TO as TAG_TO,'||unistr('\000a')||
'    PATCHES_V.SUPPLEMENTARY as SUPPLEMENTARY,'||unistr('\000a')||
'    PATCHES_V.PATCH_DESC as PATCH_DESC,'||unistr('\000a')||
'    PATCHES_V.PATCH_COMPONANTS as PATCH_COMPONANTS,'||unistr('\000a')||
'    PATCHES_V.PATCH_CREATE_DATE as PATCH_CREATE_DATE,'||unistr('\000a')||
'    PATCHES_V.PAT';

s:=s||'CH_CREATED_BY as PATCH_CREATED_BY,'||unistr('\000a')||
'    PATCHES_V.NOTE as NOTE,'||unistr('\000a')||
'    PATCHES_V.LOG_DATETIME as LOG_DATETIME,'||unistr('\000a')||
'    PATCHES_V.COMPLETED_DATETIME as COMPLETED_DATETIME,'||unistr('\000a')||
'    PATCHES_V.SUCCESS_YN as SUCCESS_YN,'||unistr('\000a')||
'    PATCHES_V.RETIRED_YN as RETIRED_YN,'||unistr('\000a')||
'    PATCHES_V.WARNING_COUNT as WARNING_COUNT,'||unistr('\000a')||
'    PATCHES_V.RERUNNABLE_YN as RERUNNABLE_YN,'||unistr('\000a')||
'    PATCHES_V.ERROR_COUNT as ERROR_COUNT,'||unistr('\000a')||
'    PATCHES_V.USERNAME ';

s:=s||'as USERNAME,'||unistr('\000a')||
'    PATCHES_V.INSTALL_LOG as INSTALL_LOG,'||unistr('\000a')||
'    PATCHES_V.CREATED_BY as CREATED_BY,'||unistr('\000a')||
'    PATCHES_V.CREATED_ON as CREATED_ON,'||unistr('\000a')||
'    PATCHES_V.LAST_UPDATED_BY as LAST_UPDATED_BY,'||unistr('\000a')||
'    PATCHES_V.PATCH_TYPE as PATCH_TYPE '||unistr('\000a')||
' from PATCHES_V PATCHES_V';

wwv_flow_api.create_page_plug (
  p_id=> 40767123845551144 + wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_plug_name=> 'Installed Patches',
  p_region_name=>'',
  p_escape_on_http_output=>'N',
  p_plug_template=> 40759520115551113+ wwv_flow_api.g_id_offset,
  p_plug_display_sequence=> 10,
  p_plug_new_grid         => false,
  p_plug_new_grid_row     => true,
  p_plug_new_grid_column  => true,
  p_plug_display_column=> null,
  p_plug_display_point=> 'BODY_3',
  p_plug_item_display_point=> 'ABOVE',
  p_plug_source=> s,
  p_plug_source_type=> 'DYNAMIC_QUERY',
  p_plug_query_row_template=> 1,
  p_plug_query_headings_type=> 'COLON_DELMITED_LIST',
  p_plug_query_row_count_max => 500,
  p_plug_display_condition_type => '',
  p_plug_caching=> 'NOT_CACHED',
  p_plug_comment=> '');
end;
/
declare
 a1 varchar2(32767) := null;
begin
a1:=a1||'select PATCHES_V.PATCH_NAME as PATCH_NAME,'||unistr('\000a')||
'    PATCHES_V.DB_SCHEMA as DB_SCHEMA,'||unistr('\000a')||
'    PATCHES_V.BRANCH_NAME as BRANCH_NAME,'||unistr('\000a')||
'    PATCHES_V.TAG_FROM as TAG_FROM,'||unistr('\000a')||
'    PATCHES_V.TAG_TO as TAG_TO,'||unistr('\000a')||
'    PATCHES_V.SUPPLEMENTARY as SUPPLEMENTARY,'||unistr('\000a')||
'    PATCHES_V.PATCH_DESC as PATCH_DESC,'||unistr('\000a')||
'    PATCHES_V.PATCH_COMPONANTS as PATCH_COMPONANTS,'||unistr('\000a')||
'    PATCHES_V.PATCH_CREATE_DATE as PATCH_CREATE_DATE,'||unistr('\000a')||
'    PATCHES_V.PAT';

a1:=a1||'CH_CREATED_BY as PATCH_CREATED_BY,'||unistr('\000a')||
'    PATCHES_V.NOTE as NOTE,'||unistr('\000a')||
'    PATCHES_V.LOG_DATETIME as LOG_DATETIME,'||unistr('\000a')||
'    PATCHES_V.COMPLETED_DATETIME as COMPLETED_DATETIME,'||unistr('\000a')||
'    PATCHES_V.SUCCESS_YN as SUCCESS_YN,'||unistr('\000a')||
'    PATCHES_V.RETIRED_YN as RETIRED_YN,'||unistr('\000a')||
'    PATCHES_V.WARNING_COUNT as WARNING_COUNT,'||unistr('\000a')||
'    PATCHES_V.RERUNNABLE_YN as RERUNNABLE_YN,'||unistr('\000a')||
'    PATCHES_V.ERROR_COUNT as ERROR_COUNT,'||unistr('\000a')||
'    PATCHES_V.USERNAME ';

a1:=a1||'as USERNAME,'||unistr('\000a')||
'    PATCHES_V.INSTALL_LOG as INSTALL_LOG,'||unistr('\000a')||
'    PATCHES_V.CREATED_BY as CREATED_BY,'||unistr('\000a')||
'    PATCHES_V.CREATED_ON as CREATED_ON,'||unistr('\000a')||
'    PATCHES_V.LAST_UPDATED_BY as LAST_UPDATED_BY,'||unistr('\000a')||
'    PATCHES_V.PATCH_TYPE as PATCH_TYPE '||unistr('\000a')||
' from PATCHES_V PATCHES_V';

wwv_flow_api.create_worksheet(
  p_id=> 40767208766551144+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_region_id=> 40767123845551144+wwv_flow_api.g_id_offset,
  p_name=> 'Installed Patches',
  p_folder_id=> null, 
  p_alias=> '',
  p_report_id_item=> '',
  p_max_row_count=> '1000000',
  p_max_row_count_message=> 'The maximum row count for this report is #MAX_ROW_COUNT# rows.  Please apply a filter to reduce the number of records in your query.',
  p_no_data_found_message=> 'No data found.',
  p_max_rows_per_page=>'',
  p_search_button_label=>'',
  p_sort_asc_image=>'',
  p_sort_asc_image_attr=>'',
  p_sort_desc_image=>'',
  p_sort_desc_image_attr=>'',
  p_sql_query => a1,
  p_status=>'AVAILABLE_FOR_OWNER',
  p_allow_report_saving=>'Y',
  p_allow_save_rpt_public=>'N',
  p_allow_report_categories=>'N',
  p_show_nulls_as=>'-',
  p_pagination_type=>'ROWS_X_TO_Y',
  p_pagination_display_pos=>'BOTTOM_RIGHT',
  p_show_finder_drop_down=>'Y',
  p_show_display_row_count=>'N',
  p_show_search_bar=>'Y',
  p_show_search_textbox=>'Y',
  p_show_actions_menu=>'Y',
  p_report_list_mode=>'TABS',
  p_show_detail_link=>'Y',
  p_show_select_columns=>'Y',
  p_show_rows_per_page=>'Y',
  p_show_filter=>'Y',
  p_show_sort=>'Y',
  p_show_control_break=>'Y',
  p_show_highlight=>'Y',
  p_show_computation=>'Y',
  p_show_aggregate=>'Y',
  p_show_chart=>'Y',
  p_show_group_by=>'Y',
  p_show_notify=>'N',
  p_show_calendar=>'N',
  p_show_flashback=>'Y',
  p_show_reset=>'Y',
  p_show_download=>'Y',
  p_show_help=>'Y',
  p_download_formats=>'CSV:HTML:EMAIL',
  p_detail_link_text=>'<img src="#IMAGE_PREFIX#menu/pencil16x16.gif" alt="" />',
  p_allow_exclude_null_values=>'Y',
  p_allow_hide_extra_columns=>'Y',
  p_icon_view_enabled_yn=>'N',
  p_icon_view_use_custom=>'N',
  p_icon_view_columns_per_row=>1,
  p_detail_view_enabled_yn=>'N',
  p_owner=>'PETER',
  p_internal_uid=> 26511904471117266);
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40767320586551147+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'PATCH_NAME',
  p_display_order          =>1,
  p_column_identifier      =>'A',
  p_column_label           =>'Patch Name',
  p_report_label           =>'Patch Name',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40767412330551147+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'DB_SCHEMA',
  p_display_order          =>2,
  p_column_identifier      =>'B',
  p_column_label           =>'Db Schema',
  p_report_label           =>'Db Schema',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40767522636551147+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'BRANCH_NAME',
  p_display_order          =>3,
  p_column_identifier      =>'C',
  p_column_label           =>'Branch Name',
  p_report_label           =>'Branch Name',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40767619646551147+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'TAG_FROM',
  p_display_order          =>4,
  p_column_identifier      =>'D',
  p_column_label           =>'Tag From',
  p_report_label           =>'Tag From',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40767729283551147+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'TAG_TO',
  p_display_order          =>5,
  p_column_identifier      =>'E',
  p_column_label           =>'Tag To',
  p_report_label           =>'Tag To',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40767822258551147+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'SUPPLEMENTARY',
  p_display_order          =>6,
  p_column_identifier      =>'F',
  p_column_label           =>'Supplementary',
  p_report_label           =>'Supplementary',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40767936989551148+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'PATCH_DESC',
  p_display_order          =>7,
  p_column_identifier      =>'G',
  p_column_label           =>'Patch Desc',
  p_report_label           =>'Patch Desc',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768016970551148+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'PATCH_COMPONANTS',
  p_display_order          =>8,
  p_column_identifier      =>'H',
  p_column_label           =>'Patch Componants',
  p_report_label           =>'Patch Componants',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'N',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'N',
  p_allow_aggregations     =>'N',
  p_allow_computations     =>'N',
  p_allow_charting         =>'N',
  p_allow_group_by         =>'N',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'CLOB',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'RIGHT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'N',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768128227551148+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'PATCH_CREATE_DATE',
  p_display_order          =>9,
  p_column_identifier      =>'I',
  p_column_label           =>'Patch Create Date',
  p_report_label           =>'Patch Create Date',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'DATE',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'RIGHT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768236762551148+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'PATCH_CREATED_BY',
  p_display_order          =>10,
  p_column_identifier      =>'J',
  p_column_label           =>'Patch Created By',
  p_report_label           =>'Patch Created By',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768321664551148+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'NOTE',
  p_display_order          =>11,
  p_column_identifier      =>'K',
  p_column_label           =>'Note',
  p_report_label           =>'Note',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768406826551148+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'LOG_DATETIME',
  p_display_order          =>12,
  p_column_identifier      =>'L',
  p_column_label           =>'Log Datetime',
  p_report_label           =>'Log Datetime',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'DATE',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'RIGHT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768506963551148+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'COMPLETED_DATETIME',
  p_display_order          =>13,
  p_column_identifier      =>'M',
  p_column_label           =>'Completed Datetime',
  p_report_label           =>'Completed Datetime',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'DATE',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'RIGHT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768607442551148+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'SUCCESS_YN',
  p_display_order          =>14,
  p_column_identifier      =>'N',
  p_column_label           =>'Success Yn',
  p_report_label           =>'Success Yn',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768728604551149+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'RETIRED_YN',
  p_display_order          =>15,
  p_column_identifier      =>'O',
  p_column_label           =>'Retired Yn',
  p_report_label           =>'Retired Yn',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768835618551149+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'WARNING_COUNT',
  p_display_order          =>16,
  p_column_identifier      =>'P',
  p_column_label           =>'Warning Count',
  p_report_label           =>'Warning Count',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'NUMBER',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'RIGHT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40768936288551149+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'RERUNNABLE_YN',
  p_display_order          =>17,
  p_column_identifier      =>'Q',
  p_column_label           =>'Rerunnable Yn',
  p_report_label           =>'Rerunnable Yn',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40769020928551149+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'ERROR_COUNT',
  p_display_order          =>18,
  p_column_identifier      =>'R',
  p_column_label           =>'Error Count',
  p_report_label           =>'Error Count',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'NUMBER',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'RIGHT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40769126549551149+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'USERNAME',
  p_display_order          =>19,
  p_column_identifier      =>'S',
  p_column_label           =>'Username',
  p_report_label           =>'Username',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40769235959551149+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'INSTALL_LOG',
  p_display_order          =>20,
  p_column_identifier      =>'T',
  p_column_label           =>'Install Log',
  p_report_label           =>'Install Log',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'N',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'N',
  p_allow_aggregations     =>'N',
  p_allow_computations     =>'N',
  p_allow_charting         =>'N',
  p_allow_group_by         =>'N',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'CLOB',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'RIGHT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'N',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40769308282551149+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'CREATED_BY',
  p_display_order          =>21,
  p_column_identifier      =>'U',
  p_column_label           =>'Created By',
  p_report_label           =>'Created By',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40769406446551149+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'CREATED_ON',
  p_display_order          =>22,
  p_column_identifier      =>'V',
  p_column_label           =>'Created On',
  p_report_label           =>'Created On',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'DATE',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'RIGHT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40769526117551150+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'LAST_UPDATED_BY',
  p_display_order          =>23,
  p_column_identifier      =>'W',
  p_column_label           =>'Last Updated By',
  p_report_label           =>'Last Updated By',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
begin
wwv_flow_api.create_worksheet_column(
  p_id => 40769635648551150+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_db_column_name         =>'PATCH_TYPE',
  p_display_order          =>24,
  p_column_identifier      =>'X',
  p_column_label           =>'Patch Type',
  p_report_label           =>'Patch Type',
  p_sync_form_label        =>'Y',
  p_display_in_default_rpt =>'Y',
  p_is_sortable            =>'Y',
  p_allow_sorting          =>'Y',
  p_allow_filtering        =>'Y',
  p_allow_highlighting     =>'Y',
  p_allow_ctrl_breaks      =>'Y',
  p_allow_aggregations     =>'Y',
  p_allow_computations     =>'Y',
  p_allow_charting         =>'Y',
  p_allow_group_by         =>'Y',
  p_allow_hide             =>'Y',
  p_others_may_edit        =>'Y',
  p_others_may_view        =>'Y',
  p_column_type            =>'STRING',
  p_display_as             =>'TEXT',
  p_display_text_as        =>'ESCAPE_SC',
  p_heading_alignment      =>'CENTER',
  p_column_alignment       =>'LEFT',
  p_tz_dependent           =>'N',
  p_rpt_distinct_lov       =>'Y',
  p_rpt_show_filter_lov    =>'D',
  p_rpt_filter_date_ranges =>'ALL',
  p_help_text              =>'No help available for this page item.');
end;
/
declare
    rc1 varchar2(32767) := null;
begin
rc1:=rc1||'PATCH_NAME:DB_SCHEMA:BRANCH_NAME:TAG_FROM:TAG_TO:SUPPLEMENTARY:PATCH_DESC:PATCH_COMPONANTS:PATCH_CREATE_DATE:PATCH_CREATED_BY:NOTE:LOG_DATETIME:COMPLETED_DATETIME:SUCCESS_YN:RETIRED_YN:WARNING_COUNT:RERUNNABLE_YN:ERROR_COUNT:USERNAME:INSTALL_LOG:CREATED_BY:CREATED_ON:LAST_UPDATED_BY:PATCH_TYPE';

wwv_flow_api.create_worksheet_rpt(
  p_id => 40770419957552453+wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_worksheet_id => 40767208766551144+wwv_flow_api.g_id_offset,
  p_session_id  => null,
  p_base_report_id  => null+wwv_flow_api.g_id_offset,
  p_application_user => 'APXWS_DEFAULT',
  p_report_seq              =>10,
  p_report_alias            =>'265152',
  p_status                  =>'PUBLIC',
  p_category_id             =>null+wwv_flow_api.g_id_offset,
  p_is_default              =>'Y',
  p_display_rows            =>15,
  p_report_columns          =>rc1,
  p_flashback_enabled       =>'N',
  p_calendar_display_column =>'');
end;
/
declare
  s varchar2(32767) := null;
  l_clob clob;
  l_length number := 1;
begin
s := null;
wwv_flow_api.create_page_plug (
  p_id=> 40769831217551151 + wwv_flow_api.g_id_offset,
  p_flow_id=> wwv_flow.g_flow_id,
  p_page_id=> 2,
  p_plug_name=> 'Breadcrumbs',
  p_region_name=>'',
  p_escape_on_http_output=>'N',
  p_plug_template=> 40758424112551113+ wwv_flow_api.g_id_offset,
  p_plug_display_sequence=> 10,
  p_plug_new_grid         => false,
  p_plug_new_grid_row     => true,
  p_plug_new_grid_column  => true,
  p_plug_display_column=> null,
  p_plug_display_point=> 'REGION_POSITION_01',
  p_plug_item_display_point=> 'ABOVE',
  p_plug_source=> s,
  p_plug_source_type=> 'M'|| to_char(40766016689551138 + wwv_flow_api.g_id_offset),
  p_menu_template_id=> 40764004659551119+ wwv_flow_api.g_id_offset,
  p_plug_query_row_template=> 1,
  p_plug_query_headings_type=> 'COLON_DELMITED_LIST',
  p_plug_query_row_count_max => 500,
  p_plug_display_condition_type => '',
  p_plug_caching=> 'NOT_CACHED',
  p_plug_comment=> '');
end;
/
 
begin
 
null;
 
end;
/

 
begin
 
null;
 
end;
/

 
begin
 
---------------------------------------
-- ...updatable report columns for page 2
--
 
begin
 
null;
end;
null;
 
end;
/

 
