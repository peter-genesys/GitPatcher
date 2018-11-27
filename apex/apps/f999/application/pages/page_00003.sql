prompt --application/pages/page_00003
begin
wwv_flow_api.create_page(
 p_id=>3
,p_user_interface_id=>wwv_flow_api.id(72818849918899195)
,p_tab_set=>'TS1'
,p_name=>'Patches Unapplied'
,p_step_title=>'Patches Unapplied'
,p_reload_on_submit=>'A'
,p_warn_on_unsaved_changes=>'N'
,p_step_sub_title=>'Patches Unapplied'
,p_step_sub_title_type=>'TEXT_WITH_SUBSTITUTIONS'
,p_autocomplete_on_off=>'ON'
,p_page_template_options=>'#DEFAULT#'
,p_nav_list_template_options=>'#DEFAULT#'
,p_help_text=>'No help is available for this page.'
,p_last_updated_by=>'PETER'
,p_last_upd_yyyymmddhh24miss=>'20180322123250'
);
wwv_flow_api.create_page_plug(
 p_id=>wwv_flow_api.id(46323656287579193)
,p_plug_name=>'Patches Unapplied - to be applied &APP_ALIAS.'
,p_region_template_options=>'#DEFAULT#:t-Region--scrollBody'
,p_component_template_options=>'#DEFAULT#'
,p_plug_template=>wwv_flow_api.id(90112942111216764)
,p_plug_display_sequence=>10
,p_plug_display_point=>'BODY_3'
,p_query_type=>'SQL'
,p_plug_source=>wwv_flow_string.join(wwv_flow_t_varchar2(
'select PATCHES_UNAPPLIED_V.PATCH_NAME as PATCH_NAME,',
'    PATCHES_UNAPPLIED_V.DB_SCHEMA as DB_SCHEMA,',
'    PATCHES_UNAPPLIED_V.BRANCH_NAME as BRANCH_NAME,',
'    PATCHES_UNAPPLIED_V.TAG_FROM as TAG_FROM,',
'    PATCHES_UNAPPLIED_V.TAG_TO as TAG_TO,',
'    PATCHES_UNAPPLIED_V.SUPPLEMENTARY as SUPPLEMENTARY,',
'    PATCHES_UNAPPLIED_V.PATCH_DESC as PATCH_DESC,',
'    PATCHES_UNAPPLIED_V.PATCH_CREATE_DATE as PATCH_CREATE_DATE,',
'    PATCHES_UNAPPLIED_V.PATCH_CREATED_BY as PATCH_CREATED_BY,',
'    PATCHES_UNAPPLIED_V.NOTE as NOTE,',
'    PATCHES_UNAPPLIED_V.RERUNNABLE_YN as RERUNNABLE_YN ',
' from PATCHES_UNAPPLIED_V PATCHES_UNAPPLIED_V'))
,p_plug_source_type=>'NATIVE_IR'
,p_plug_query_options=>'DERIVED_REPORT_COLUMNS'
);
wwv_flow_api.create_worksheet(
 p_id=>wwv_flow_api.id(46323743950579193)
,p_name=>'Patches Unapplied'
,p_max_row_count=>'1000000'
,p_max_row_count_message=>'The maximum row count for this report is #MAX_ROW_COUNT# rows.  Please apply a filter to reduce the number of records in your query.'
,p_no_data_found_message=>'No data found.'
,p_show_nulls_as=>'-'
,p_pagination_type=>'ROWS_X_TO_Y'
,p_pagination_display_pos=>'BOTTOM_RIGHT'
,p_report_list_mode=>'TABS'
,p_fixed_header=>'NONE'
,p_show_detail_link=>'N'
,p_show_pivot=>'N'
,p_download_formats=>'CSV:HTML:EMAIL'
,p_owner=>'PETER'
,p_internal_uid=>14269604644231123
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46324268826579376)
,p_db_column_name=>'PATCH_NAME'
,p_display_order=>1
,p_column_identifier=>'A'
,p_column_label=>'Patch Name'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_NAME'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46324358409579378)
,p_db_column_name=>'DB_SCHEMA'
,p_display_order=>2
,p_column_identifier=>'B'
,p_column_label=>'Db Schema'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'DB_SCHEMA'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46324466146579378)
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
 p_id=>wwv_flow_api.id(46324553321579378)
,p_db_column_name=>'TAG_FROM'
,p_display_order=>4
,p_column_identifier=>'D'
,p_column_label=>'Tag From'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'TAG_FROM'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46324650673579378)
,p_db_column_name=>'TAG_TO'
,p_display_order=>5
,p_column_identifier=>'E'
,p_column_label=>'Tag To'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'TAG_TO'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46324749046579378)
,p_db_column_name=>'SUPPLEMENTARY'
,p_display_order=>6
,p_column_identifier=>'F'
,p_column_label=>'Supplementary'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'SUPPLEMENTARY'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46324860939579378)
,p_db_column_name=>'PATCH_DESC'
,p_display_order=>7
,p_column_identifier=>'G'
,p_column_label=>'Patch Desc'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_DESC'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46324951149579378)
,p_db_column_name=>'PATCH_CREATE_DATE'
,p_display_order=>8
,p_column_identifier=>'H'
,p_column_label=>'Patch Create Date'
,p_allow_pivot=>'N'
,p_column_type=>'DATE'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_CREATE_DATE'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46325045380579379)
,p_db_column_name=>'PATCH_CREATED_BY'
,p_display_order=>9
,p_column_identifier=>'I'
,p_column_label=>'Patch Created By'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'PATCH_CREATED_BY'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46325152471579379)
,p_db_column_name=>'NOTE'
,p_display_order=>10
,p_column_identifier=>'J'
,p_column_label=>'Note'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'NOTE'
);
wwv_flow_api.create_worksheet_column(
 p_id=>wwv_flow_api.id(46325258324579379)
,p_db_column_name=>'RERUNNABLE_YN'
,p_display_order=>11
,p_column_identifier=>'K'
,p_column_label=>'Rerunnable Yn'
,p_allow_pivot=>'N'
,p_column_type=>'STRING'
,p_tz_dependent=>'N'
,p_static_id=>'RERUNNABLE_YN'
);
wwv_flow_api.create_worksheet_rpt(
 p_id=>wwv_flow_api.id(46326150365647019)
,p_application_user=>'APXWS_DEFAULT'
,p_report_seq=>10
,p_report_alias=>'142721'
,p_status=>'PUBLIC'
,p_is_default=>'Y'
,p_display_rows=>15
,p_report_columns=>'PATCH_NAME:PATCH_DESC:NOTE:DB_SCHEMA:PATCH_CREATE_DATE:PATCH_CREATED_BY:RERUNNABLE_YN:'
);
wwv_flow_api.create_page_plug(
 p_id=>wwv_flow_api.id(50537147360115221)
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
