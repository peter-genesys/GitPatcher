prompt --application/pages/page_00004
begin
wwv_flow_api.create_page(
 p_id=>4
,p_user_interface_id=>wwv_flow_api.id(72818849918899195)
,p_tab_set=>'TS1'
,p_name=>'Patches Unpromoted'
,p_step_title=>'Patches Unpromoted'
,p_reload_on_submit=>'A'
,p_warn_on_unsaved_changes=>'N'
,p_step_sub_title=>'Patches Uninstalled'
,p_step_sub_title_type=>'TEXT_WITH_SUBSTITUTIONS'
,p_first_item=>'AUTO_FIRST_ITEM'
,p_autocomplete_on_off=>'ON'
,p_page_template_options=>'#DEFAULT#'
,p_nav_list_template_options=>'#DEFAULT#'
,p_help_text=>'No help is available for this page.'
,p_last_updated_by=>'BURGESPE'
,p_last_upd_yyyymmddhh24miss=>'20170503160645'
);
wwv_flow_api.create_page_plug(
 p_id=>wwv_flow_api.id(46326951901698553)
,p_plug_name=>'Patches Unpromoted'
,p_region_template_options=>'#DEFAULT#:t-Region--scrollBody'
,p_component_template_options=>'#DEFAULT#'
,p_plug_template=>wwv_flow_api.id(90112942111216764)
,p_plug_display_sequence=>10
,p_plug_display_point=>'BODY_3'
,p_query_type=>'SQL'
,p_plug_source=>wwv_flow_string.join(wwv_flow_t_varchar2(
'select PATCHES_UNPROMOTED_V.PATCH_ID as PATCH_ID,',
'    PATCHES_UNPROMOTED_V.PATCH_NAME as PATCH_NAME,',
'    PATCHES_UNPROMOTED_V.BRANCH_NAME as BRANCH_NAME,',
'    PATCHES_UNPROMOTED_V.DB_SCHEMA as DB_SCHEMA,',
'    PATCHES_UNPROMOTED_V.TAG_FROM as TAG_FROM,',
'    PATCHES_UNPROMOTED_V.TAG_TO as TAG_TO,',
'    PATCHES_UNPROMOTED_V.SUPPLEMENTARY as SUPPLEMENTARY,',
'    PATCHES_UNPROMOTED_V.PATCH_DESC as PATCH_DESC,',
'REPLACE(PATCHES_UNPROMOTED_V.PATCH_COMPONANTS,'','',''<BR>'') as PATCH_COMPONANTS,',
'    PATCHES_UNPROMOTED_V.PATCH_CREATE_DATE as PATCH_CREATE_DATE,',
'    PATCHES_UNPROMOTED_V.NOTE as NOTE,',
'    PATCHES_UNPROMOTED_V.PATCH_CREATED_BY as PATCH_CREATED_BY,',
'    PATCHES_UNPROMOTED_V.LOG_DATETIME as LOG_DATETIME,',
'    PATCHES_UNPROMOTED_V.COMPLETED_DATETIME as COMPLETED_DATETIME,',
'    PATCHES_UNPROMOTED_V.SUCCESS_YN as SUCCESS_YN,',
'    PATCHES_UNPROMOTED_V.RETIRED_YN as RETIRED_YN,',
'    PATCHES_UNPROMOTED_V.RERUNNABLE_YN as RERUNNABLE_YN,',
'    PATCHES_UNPROMOTED_V.WARNING_COUNT as WARNING_COUNT,',
'    PATCHES_UNPROMOTED_V.ERROR_COUNT as ERROR_COUNT,',
'    PATCHES_UNPROMOTED_V.USERNAME as USERNAME,',
'    PATCHES_UNPROMOTED_V.INSTALL_LOG as INSTALL_LOG,',
'    PATCHES_UNPROMOTED_V.CREATED_BY as CREATED_BY,',
'    PATCHES_UNPROMOTED_V.CREATED_ON as CREATED_ON,',
'    PATCHES_UNPROMOTED_V.LAST_UPDATED_BY as LAST_UPDATED_BY,',
'    PATCHES_UNPROMOTED_V.LAST_UPDATED_ON as LAST_UPDATED_ON,',
'    PATCHES_UNPROMOTED_V.PATCH_TYPE as PATCH_TYPE ',
' from PATCHES_UNPROMOTED_V PATCHES_UNPROMOTED_V'))
,p_plug_source_type=>'NATIVE_IR'
);
wwv_flow_api.create_worksheet(
 p_id=>wwv_flow_api.id(46327066044698553)
,p_name=>'Patches Uninstalled'
,p_max_row_count=>'1000000'
,p_max_row_count_message=>'The maximum row count for this report is #MAX_ROW_COUNT# rows.  Please apply a filter to reduce the number of records in your query.'
,p_no_data_found_message=>'No data found.'
,p_allow_report_categories=>'N'
,p_show_nulls_as=>'-'
,p_pagination_type=>'ROWS_X_TO_Y'
,p_pagination_display_pos=>'BOTTOM_RIGHT'
,p_report_list_mode=>'TABS'
,p_fixed_header=>'NONE'
,p_show_detail_link=>'N'
,p_show_pivot=>'N'
,p_show_calendar=>'N'
,p_download_formats=>'CSV:HTML:EMAIL'
,p_allow_exclude_null_values=>'N'
,p_allow_hide_extra_columns=>'N'
,p_icon_view_columns_per_row=>1
,p_owner=>'PETER'
,p_internal_uid=>14272926738350483
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46327251629698736)
,p_db_column_name=>'PATCH_ID'
,p_display_order=>1
,p_column_identifier=>'A'
,p_column_label=>'Patch Id'
,p_allow_pivot=>'N'
,p_column_type=>'NUMBER'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_ID'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46327356972698737)
,p_db_column_name=>'PATCH_NAME'
,p_display_order=>2
,p_column_identifier=>'B'
,p_column_label=>'Patch Name'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_NAME'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46327464178698738)
,p_db_column_name=>'BRANCH_NAME'
,p_display_order=>3
,p_column_identifier=>'C'
,p_column_label=>'Branch Name'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'BRANCH_NAME'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46327544296698738)
,p_db_column_name=>'DB_SCHEMA'
,p_display_order=>4
,p_column_identifier=>'D'
,p_column_label=>'Db Schema'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'DB_SCHEMA'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46327670384698738)
,p_db_column_name=>'TAG_FROM'
,p_display_order=>5
,p_column_identifier=>'E'
,p_column_label=>'Tag From'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'TAG_FROM'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46327756156698738)
,p_db_column_name=>'TAG_TO'
,p_display_order=>6
,p_column_identifier=>'F'
,p_column_label=>'Tag To'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'TAG_TO'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46327860150698738)
,p_db_column_name=>'SUPPLEMENTARY'
,p_display_order=>7
,p_column_identifier=>'G'
,p_column_label=>'Supplementary'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'SUPPLEMENTARY'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46327950137698738)
,p_db_column_name=>'PATCH_DESC'
,p_display_order=>8
,p_column_identifier=>'H'
,p_column_label=>'Patch Desc'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_DESC'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328054121698738)
,p_db_column_name=>'PATCH_COMPONANTS'
,p_display_order=>9
,p_column_identifier=>'I'
,p_column_label=>'Patch Componants'
,p_allow_sorting=>'N'
,p_allow_ctrl_breaks=>'N'
,p_allow_aggregations=>'N'
,p_allow_computations=>'N'
,p_allow_charting=>'N'
,p_allow_group_by=>'N'
,p_allow_pivot=>'N'
,p_column_type=>'CLOB'
,p_display_text_as=>'WITHOUT_MODIFICATION'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_COMPONANTS'
,p_rpt_show_filter_lov=>'N'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328161743698739)
,p_db_column_name=>'PATCH_CREATE_DATE'
,p_display_order=>10
,p_column_identifier=>'J'
,p_column_label=>'Patch Create Date'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_CREATE_DATE'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328242379698739)
,p_db_column_name=>'NOTE'
,p_display_order=>11
,p_column_identifier=>'K'
,p_column_label=>'Note'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'NOTE'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328371700698739)
,p_db_column_name=>'PATCH_CREATED_BY'
,p_display_order=>12
,p_column_identifier=>'L'
,p_column_label=>'Patch Created By'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_CREATED_BY'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328472025698739)
,p_db_column_name=>'LOG_DATETIME'
,p_display_order=>13
,p_column_identifier=>'M'
,p_column_label=>'Log Datetime'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_tz_dependent=>'N'
,p_static_id=>'LOG_DATETIME'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328549671698739)
,p_db_column_name=>'COMPLETED_DATETIME'
,p_display_order=>14
,p_column_identifier=>'N'
,p_column_label=>'Completed Datetime'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_tz_dependent=>'N'
,p_static_id=>'COMPLETED_DATETIME'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328650991698739)
,p_db_column_name=>'SUCCESS_YN'
,p_display_order=>15
,p_column_identifier=>'O'
,p_column_label=>'Success Yn'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'SUCCESS_YN'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328769370698739)
,p_db_column_name=>'RETIRED_YN'
,p_display_order=>16
,p_column_identifier=>'P'
,p_column_label=>'Retired Yn'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'RETIRED_YN'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328862462698740)
,p_db_column_name=>'RERUNNABLE_YN'
,p_display_order=>17
,p_column_identifier=>'Q'
,p_column_label=>'Rerunnable Yn'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'RERUNNABLE_YN'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46328952693698740)
,p_db_column_name=>'WARNING_COUNT'
,p_display_order=>18
,p_column_identifier=>'R'
,p_column_label=>'Warning Count'
,p_allow_pivot=>'N'
,p_column_type=>'NUMBER'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'WARNING_COUNT'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46329063088698740)
,p_db_column_name=>'ERROR_COUNT'
,p_display_order=>19
,p_column_identifier=>'S'
,p_column_label=>'Error Count'
,p_allow_pivot=>'N'
,p_column_type=>'NUMBER'
,p_column_alignment=>'RIGHT'
,p_tz_dependent=>'N'
,p_static_id=>'ERROR_COUNT'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46329162471698740)
,p_db_column_name=>'USERNAME'
,p_display_order=>20
,p_column_identifier=>'T'
,p_column_label=>'Username'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'USERNAME'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46329263632698740)
,p_db_column_name=>'INSTALL_LOG'
,p_display_order=>21
,p_column_identifier=>'U'
,p_column_label=>'Install Log'
,p_allow_sorting=>'N'
,p_allow_ctrl_breaks=>'N'
,p_allow_aggregations=>'N'
,p_allow_computations=>'N'
,p_allow_charting=>'N'
,p_allow_group_by=>'N'
,p_allow_pivot=>'N'
,p_column_type=>'CLOB'
,p_tz_dependent=>'N'
,p_static_id=>'INSTALL_LOG'
,p_rpt_show_filter_lov=>'N'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46329349828698740)
,p_db_column_name=>'CREATED_BY'
,p_display_order=>22
,p_column_identifier=>'V'
,p_column_label=>'Created By'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'CREATED_BY'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46329458574698740)
,p_db_column_name=>'CREATED_ON'
,p_display_order=>23
,p_column_identifier=>'W'
,p_column_label=>'Created On'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_tz_dependent=>'N'
,p_static_id=>'CREATED_ON'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46329548064698741)
,p_db_column_name=>'LAST_UPDATED_BY'
,p_display_order=>24
,p_column_identifier=>'X'
,p_column_label=>'Last Updated By'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'LAST_UPDATED_BY'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46329648764698741)
,p_db_column_name=>'LAST_UPDATED_ON'
,p_display_order=>25
,p_column_identifier=>'Y'
,p_column_label=>'Last Updated On'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_tz_dependent=>'N'
,p_static_id=>'LAST_UPDATED_ON'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46329764774698741)
,p_db_column_name=>'PATCH_TYPE'
,p_display_order=>26
,p_column_identifier=>'Z'
,p_column_label=>'Patch Type'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_TYPE'
);
wwv_flow_api.create_worksheet_rpt(
 p_id=>wwv_flow_api.id(46329961887707098)
,p_application_user=>'APXWS_DEFAULT'
,p_report_seq=>10
,p_report_alias=>'142759'
,p_status=>'PUBLIC'
,p_is_default=>'Y'
,p_display_rows=>15
,p_report_columns=>'PATCH_TYPE:PATCH_NAME:PATCH_DESC:NOTE:PATCH_CREATE_DATE:PATCH_CREATED_BY:COMPLETED_DATETIME:PATCH_COMPONANTS:'
,p_break_on=>'PATCH_TYPE:0:0:0:0:0'
,p_break_enabled_on=>'PATCH_TYPE:0:0:0:0:0'
);
wwv_flow_api.create_page_plug(
 p_id=>wwv_flow_api.id(50535455194108111)
,p_plug_name=>'Breadcrumbs'
,p_region_template_options=>'#DEFAULT#:t-BreadcrumbRegion--useBreadcrumbTitle'
,p_component_template_options=>'#DEFAULT#'
,p_plug_template=>wwv_flow_api.id(90119950620216768)
,p_plug_display_sequence=>20
,p_plug_display_point=>'REGION_POSITION_01'
,p_menu_id=>wwv_flow_api.id(72820155995899208)
,p_plug_source_type=>'NATIVE_BREADCRUMB'
,p_menu_template_id=>wwv_flow_api.id(90155628322216800)
);
end;
/
