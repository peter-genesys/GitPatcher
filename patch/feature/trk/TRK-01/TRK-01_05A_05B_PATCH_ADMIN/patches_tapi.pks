CREATE OR REPLACE PACKAGE patches_tapi IS

----------------------------------------------------------------
-- Used by COLLECTION FUNCTIONS
----------------------------------------------------------------

TYPE patches_aat IS TABLE OF PATCHES%ROWTYPE
   INDEX BY BINARY_INTEGER;


-----------------------------------------------------------------
-- RECORD FUNCTIONS
-----------------------------------------------------------------

-----------------------------------------------------------------
-- patches_pk - one row from primary key
-----------------------------------------------------------------
FUNCTION patches_pk (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES%ROWTYPE;

-----------------------------------------------------------------
-- patches_uk2 - one row from unique index
-----------------------------------------------------------------
FUNCTION patches_uk2 (
   i_db_schema IN PATCHES.DB_SCHEMA%TYPE,
   i_branch_name IN PATCHES.BRANCH_NAME%TYPE,
   i_tag_from IN PATCHES.TAG_FROM%TYPE,
   i_tag_to IN PATCHES.TAG_TO%TYPE,
   i_supplementary IN PATCHES.SUPPLEMENTARY%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES%ROWTYPE;

-----------------------------------------------------------------
-- patches_uk1 - one row from unique index
-----------------------------------------------------------------
FUNCTION patches_uk1 (
   i_patch_name IN PATCHES.PATCH_NAME%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES%ROWTYPE;



-----------------------------------------------------------------
-- COLUMN FUNCTIONS
-----------------------------------------------------------------


-----------------------------------------------------------------
-- patch_name - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_name%TYPE;





-----------------------------------------------------------------
-- patch_name - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_name (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_name%TYPE;




-----------------------------------------------------------------
-- patch_name - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_name%TYPE;





-----------------------------------------------------------------
-- db_schema - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION db_schema (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.db_schema%TYPE;





-----------------------------------------------------------------
-- db_schema - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION db_schema (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.db_schema%TYPE;




-----------------------------------------------------------------
-- db_schema - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION db_schema (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.db_schema%TYPE;





-----------------------------------------------------------------
-- branch_name - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION branch_name (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.branch_name%TYPE;





-----------------------------------------------------------------
-- branch_name - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION branch_name (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.branch_name%TYPE;




-----------------------------------------------------------------
-- branch_name - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION branch_name (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.branch_name%TYPE;





-----------------------------------------------------------------
-- tag_from - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tag_from (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_from%TYPE;





-----------------------------------------------------------------
-- tag_from - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION tag_from (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_from%TYPE;




-----------------------------------------------------------------
-- tag_from - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tag_from (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_from%TYPE;





-----------------------------------------------------------------
-- tag_to - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tag_to (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_to%TYPE;





-----------------------------------------------------------------
-- tag_to - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION tag_to (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_to%TYPE;




-----------------------------------------------------------------
-- tag_to - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tag_to (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_to%TYPE;





-----------------------------------------------------------------
-- supplementary - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION supplementary (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.supplementary%TYPE;





-----------------------------------------------------------------
-- supplementary - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION supplementary (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.supplementary%TYPE;




-----------------------------------------------------------------
-- supplementary - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION supplementary (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.supplementary%TYPE;





-----------------------------------------------------------------
-- patch_desc - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_desc%TYPE;





-----------------------------------------------------------------
-- patch_desc - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_desc%TYPE;




-----------------------------------------------------------------
-- patch_desc - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_desc%TYPE;





-----------------------------------------------------------------
-- patch_componants - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_componants%TYPE;





-----------------------------------------------------------------
-- patch_componants - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_componants%TYPE;




-----------------------------------------------------------------
-- patch_componants - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_componants%TYPE;





-----------------------------------------------------------------
-- patch_create_date - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_create_date%TYPE;





-----------------------------------------------------------------
-- patch_create_date - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_create_date%TYPE;




-----------------------------------------------------------------
-- patch_create_date - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_create_date%TYPE;





-----------------------------------------------------------------
-- patch_created_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_created_by%TYPE;





-----------------------------------------------------------------
-- patch_created_by - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_created_by%TYPE;




-----------------------------------------------------------------
-- patch_created_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_created_by%TYPE;





-----------------------------------------------------------------
-- note - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION note (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.note%TYPE;





-----------------------------------------------------------------
-- note - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION note (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.note%TYPE;




-----------------------------------------------------------------
-- note - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION note (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.note%TYPE;





-----------------------------------------------------------------
-- log_datetime - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.log_datetime%TYPE;





-----------------------------------------------------------------
-- log_datetime - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.log_datetime%TYPE;




-----------------------------------------------------------------
-- log_datetime - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.log_datetime%TYPE;





-----------------------------------------------------------------
-- completed_datetime - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.completed_datetime%TYPE;





-----------------------------------------------------------------
-- completed_datetime - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.completed_datetime%TYPE;




-----------------------------------------------------------------
-- completed_datetime - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.completed_datetime%TYPE;





-----------------------------------------------------------------
-- success_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION success_yn (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.success_yn%TYPE;





-----------------------------------------------------------------
-- success_yn - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION success_yn (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.success_yn%TYPE;




-----------------------------------------------------------------
-- success_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION success_yn (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.success_yn%TYPE;





-----------------------------------------------------------------
-- retired_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.retired_yn%TYPE;





-----------------------------------------------------------------
-- retired_yn - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.retired_yn%TYPE;




-----------------------------------------------------------------
-- retired_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.retired_yn%TYPE;





-----------------------------------------------------------------
-- rerunnable_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.rerunnable_yn%TYPE;





-----------------------------------------------------------------
-- rerunnable_yn - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.rerunnable_yn%TYPE;




-----------------------------------------------------------------
-- rerunnable_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.rerunnable_yn%TYPE;





-----------------------------------------------------------------
-- warning_count - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION warning_count (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.warning_count%TYPE;





-----------------------------------------------------------------
-- warning_count - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION warning_count (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.warning_count%TYPE;




-----------------------------------------------------------------
-- warning_count - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION warning_count (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.warning_count%TYPE;





-----------------------------------------------------------------
-- error_count - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION error_count (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.error_count%TYPE;





-----------------------------------------------------------------
-- error_count - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION error_count (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.error_count%TYPE;




-----------------------------------------------------------------
-- error_count - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION error_count (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.error_count%TYPE;





-----------------------------------------------------------------
-- username - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION username (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.username%TYPE;





-----------------------------------------------------------------
-- username - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION username (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.username%TYPE;




-----------------------------------------------------------------
-- username - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION username (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.username%TYPE;





-----------------------------------------------------------------
-- install_log - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION install_log (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.install_log%TYPE;





-----------------------------------------------------------------
-- install_log - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION install_log (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.install_log%TYPE;




-----------------------------------------------------------------
-- install_log - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION install_log (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.install_log%TYPE;





-----------------------------------------------------------------
-- created_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_by%TYPE;





-----------------------------------------------------------------
-- created_by - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION created_by (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_by%TYPE;




-----------------------------------------------------------------
-- created_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_by%TYPE;





-----------------------------------------------------------------
-- created_on - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_on%TYPE;





-----------------------------------------------------------------
-- created_on - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION created_on (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_on%TYPE;




-----------------------------------------------------------------
-- created_on - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_on%TYPE;





-----------------------------------------------------------------
-- last_updated_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_by%TYPE;





-----------------------------------------------------------------
-- last_updated_by - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_by%TYPE;




-----------------------------------------------------------------
-- last_updated_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_by%TYPE;





-----------------------------------------------------------------
-- last_updated_on - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_on%TYPE;





-----------------------------------------------------------------
-- last_updated_on - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_on%TYPE;




-----------------------------------------------------------------
-- last_updated_on - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_on%TYPE;





-----------------------------------------------------------------
-- patch_type - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_type (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_type%TYPE;





-----------------------------------------------------------------
-- patch_type - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_type (
   i_db_schema IN patches.db_schema%TYPE,
   i_branch_name IN patches.branch_name%TYPE,
   i_tag_from IN patches.tag_from%TYPE,
   i_tag_to IN patches.tag_to%TYPE,
   i_supplementary IN patches.supplementary%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_type%TYPE;




-----------------------------------------------------------------
-- patch_type - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_type (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_type%TYPE;





-----------------------------------------------------------------
-- get_current_rec
-----------------------------------------------------------------
-- get the current record by pk if given, otherwise by uk1
-----------------------------------------------------------------

FUNCTION get_current_rec(
             i_PATCHES  in PATCHES%rowtype
            ,i_raise_error IN VARCHAR2 DEFAULT 'Y'
) RETURN PATCHES%rowtype;
-----------------------------------------------------------------
-- create_rec
-----------------------------------------------------------------
-- create a record from its component fields
-----------------------------------------------------------------

FUNCTION create_rec(
             i_patch_id  in patches.patch_id%type DEFAULT NULL,
             i_patch_name  in patches.patch_name%type DEFAULT NULL,
             i_db_schema  in patches.db_schema%type DEFAULT NULL,
             i_branch_name  in patches.branch_name%type DEFAULT NULL,
             i_tag_from  in patches.tag_from%type DEFAULT NULL,
             i_tag_to  in patches.tag_to%type DEFAULT NULL,
             i_supplementary  in patches.supplementary%type DEFAULT NULL,
             i_patch_desc  in patches.patch_desc%type DEFAULT NULL,
             i_patch_componants  in patches.patch_componants%type DEFAULT NULL,
             i_patch_create_date  in patches.patch_create_date%type DEFAULT NULL,
             i_patch_created_by  in patches.patch_created_by%type DEFAULT NULL,
             i_note  in patches.note%type DEFAULT NULL,
             i_log_datetime  in patches.log_datetime%type DEFAULT NULL,
             i_completed_datetime  in patches.completed_datetime%type DEFAULT NULL,
             i_success_yn  in patches.success_yn%type DEFAULT NULL,
             i_retired_yn  in patches.retired_yn%type DEFAULT NULL,
             i_rerunnable_yn  in patches.rerunnable_yn%type DEFAULT NULL,
             i_warning_count  in patches.warning_count%type DEFAULT NULL,
             i_error_count  in patches.error_count%type DEFAULT NULL,
             i_username  in patches.username%type DEFAULT NULL,
             i_install_log  in patches.install_log%type DEFAULT NULL,
             i_created_by  in patches.created_by%type DEFAULT NULL,
             i_created_on  in patches.created_on%type DEFAULT NULL,
             i_last_updated_by  in patches.last_updated_by%type DEFAULT NULL,
             i_last_updated_on  in patches.last_updated_on%type DEFAULT NULL,
             i_patch_type  in patches.patch_type%type DEFAULT NULL
) RETURN PATCHES%rowtype;


-----------------------------------------------------------------
-- split_rec
-----------------------------------------------------------------
-- split a record into its component fields
-----------------------------------------------------------------

PROCEDURE split_rec( i_patches in patches%rowtype,
             o_patch_id  out patches.patch_id%type,
             o_patch_name  out patches.patch_name%type,
             o_db_schema  out patches.db_schema%type,
             o_branch_name  out patches.branch_name%type,
             o_tag_from  out patches.tag_from%type,
             o_tag_to  out patches.tag_to%type,
             o_supplementary  out patches.supplementary%type,
             o_patch_desc  out patches.patch_desc%type,
             o_patch_componants  out patches.patch_componants%type,
             o_patch_create_date  out patches.patch_create_date%type,
             o_patch_created_by  out patches.patch_created_by%type,
             o_note  out patches.note%type,
             o_log_datetime  out patches.log_datetime%type,
             o_completed_datetime  out patches.completed_datetime%type,
             o_success_yn  out patches.success_yn%type,
             o_retired_yn  out patches.retired_yn%type,
             o_rerunnable_yn  out patches.rerunnable_yn%type,
             o_warning_count  out patches.warning_count%type,
             o_error_count  out patches.error_count%type,
             o_username  out patches.username%type,
             o_install_log  out patches.install_log%type,
             o_created_by  out patches.created_by%type,
             o_created_on  out patches.created_on%type,
             o_last_updated_by  out patches.last_updated_by%type,
             o_last_updated_on  out patches.last_updated_on%type,
             o_patch_type  out patches.patch_type%type

);


-----------------------------------------------------------------
-- merge_old_and_new
-----------------------------------------------------------------
-- null values in NEW replaced with values from OLD
-----------------------------------------------------------------

PROCEDURE merge_old_and_new(i_old_rec  IN     patches%rowtype
                           ,io_new_rec IN OUT patches%rowtype);

------------------------------------------------------------------------------
-- INSERT
------------------------------------------------------------------------------
-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using record type, returning record
-----------------------------------------------------------------
PROCEDURE ins(
    io_patches  in out patches%rowtype ) ;
-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using components, returning components
-----------------------------------------------------------------

PROCEDURE ins(
             io_patch_id  in out patches.patch_id%type,
             io_patch_name  in out patches.patch_name%type,
             io_db_schema  in out patches.db_schema%type,
             io_branch_name  in out patches.branch_name%type,
             io_tag_from  in out patches.tag_from%type,
             io_tag_to  in out patches.tag_to%type,
             io_supplementary  in out patches.supplementary%type,
             io_patch_desc  in out patches.patch_desc%type,
             io_patch_componants  in out patches.patch_componants%type,
             io_patch_create_date  in out patches.patch_create_date%type,
             io_patch_created_by  in out patches.patch_created_by%type,
             io_note  in out patches.note%type,
             io_log_datetime  in out patches.log_datetime%type,
             io_completed_datetime  in out patches.completed_datetime%type,
             io_success_yn  in out patches.success_yn%type,
             io_retired_yn  in out patches.retired_yn%type,
             io_rerunnable_yn  in out patches.rerunnable_yn%type,
             io_warning_count  in out patches.warning_count%type,
             io_error_count  in out patches.error_count%type,
             io_username  in out patches.username%type,
             io_install_log  in out patches.install_log%type,
             io_created_by  in out patches.created_by%type,
             io_created_on  in out patches.created_on%type,
             io_last_updated_by  in out patches.last_updated_by%type,
             io_last_updated_on  in out patches.last_updated_on%type,
             io_patch_type  in out patches.patch_type%type
) ;

-----------------------------------------------------------------
-- ins_opt
-----------------------------------------------------------------
-- insert a record - using components, all optional
-----------------------------------------------------------------
PROCEDURE ins_opt(
             i_patch_id  in patches.patch_id%type DEFAULT NULL,
             i_patch_name  in patches.patch_name%type DEFAULT NULL,
             i_db_schema  in patches.db_schema%type DEFAULT NULL,
             i_branch_name  in patches.branch_name%type DEFAULT NULL,
             i_tag_from  in patches.tag_from%type DEFAULT NULL,
             i_tag_to  in patches.tag_to%type DEFAULT NULL,
             i_supplementary  in patches.supplementary%type DEFAULT NULL,
             i_patch_desc  in patches.patch_desc%type DEFAULT NULL,
             i_patch_componants  in patches.patch_componants%type DEFAULT NULL,
             i_patch_create_date  in patches.patch_create_date%type DEFAULT NULL,
             i_patch_created_by  in patches.patch_created_by%type DEFAULT NULL,
             i_note  in patches.note%type DEFAULT NULL,
             i_log_datetime  in patches.log_datetime%type DEFAULT NULL,
             i_completed_datetime  in patches.completed_datetime%type DEFAULT NULL,
             i_success_yn  in patches.success_yn%type DEFAULT NULL,
             i_retired_yn  in patches.retired_yn%type DEFAULT NULL,
             i_rerunnable_yn  in patches.rerunnable_yn%type DEFAULT NULL,
             i_warning_count  in patches.warning_count%type DEFAULT NULL,
             i_error_count  in patches.error_count%type DEFAULT NULL,
             i_username  in patches.username%type DEFAULT NULL,
             i_install_log  in patches.install_log%type DEFAULT NULL,
             i_created_by  in patches.created_by%type DEFAULT NULL,
             i_created_on  in patches.created_on%type DEFAULT NULL,
             i_last_updated_by  in patches.last_updated_by%type DEFAULT NULL,
             i_last_updated_on  in patches.last_updated_on%type DEFAULT NULL,
             i_patch_type  in patches.patch_type%type DEFAULT NULL
);
------------------------------------------------------------------------------
-- UPDATE
------------------------------------------------------------------------------
-----------------------------------------------------------------
-- upd
-----------------------------------------------------------------
-- update a record - using record type, returning record
-----------------------------------------------------------------
PROCEDURE upd(
    io_patches  in out patches%rowtype )  ;
-----------------------------------------------------------------
-- upd
-----------------------------------------------------------------
-- update a record - using components, returning components
-----------------------------------------------------------------
PROCEDURE upd(
             io_patch_id  in out patches.patch_id%type,
             io_patch_name  in out patches.patch_name%type,
             io_db_schema  in out patches.db_schema%type,
             io_branch_name  in out patches.branch_name%type,
             io_tag_from  in out patches.tag_from%type,
             io_tag_to  in out patches.tag_to%type,
             io_supplementary  in out patches.supplementary%type,
             io_patch_desc  in out patches.patch_desc%type,
             io_patch_componants  in out patches.patch_componants%type,
             io_patch_create_date  in out patches.patch_create_date%type,
             io_patch_created_by  in out patches.patch_created_by%type,
             io_note  in out patches.note%type,
             io_log_datetime  in out patches.log_datetime%type,
             io_completed_datetime  in out patches.completed_datetime%type,
             io_success_yn  in out patches.success_yn%type,
             io_retired_yn  in out patches.retired_yn%type,
             io_rerunnable_yn  in out patches.rerunnable_yn%type,
             io_warning_count  in out patches.warning_count%type,
             io_error_count  in out patches.error_count%type,
             io_username  in out patches.username%type,
             io_install_log  in out patches.install_log%type,
             io_created_by  in out patches.created_by%type,
             io_created_on  in out patches.created_on%type,
             io_last_updated_by  in out patches.last_updated_by%type,
             io_last_updated_on  in out patches.last_updated_on%type,
             io_patch_type  in out patches.patch_type%type
) ;
-------------------------------------------------------------------------
-- upd_not_null
-------------------------------------------------------------------------
-- update a record
--   using components, all optional
--   by pk if given, otherwise by uk1, null values ignored.
-----------------------------------------------------------------
PROCEDURE upd_not_null(
             i_patch_id  in patches.patch_id%type DEFAULT NULL,
             i_patch_name  in patches.patch_name%type DEFAULT NULL,
             i_db_schema  in patches.db_schema%type DEFAULT NULL,
             i_branch_name  in patches.branch_name%type DEFAULT NULL,
             i_tag_from  in patches.tag_from%type DEFAULT NULL,
             i_tag_to  in patches.tag_to%type DEFAULT NULL,
             i_supplementary  in patches.supplementary%type DEFAULT NULL,
             i_patch_desc  in patches.patch_desc%type DEFAULT NULL,
             i_patch_componants  in patches.patch_componants%type DEFAULT NULL,
             i_patch_create_date  in patches.patch_create_date%type DEFAULT NULL,
             i_patch_created_by  in patches.patch_created_by%type DEFAULT NULL,
             i_note  in patches.note%type DEFAULT NULL,
             i_log_datetime  in patches.log_datetime%type DEFAULT NULL,
             i_completed_datetime  in patches.completed_datetime%type DEFAULT NULL,
             i_success_yn  in patches.success_yn%type DEFAULT NULL,
             i_retired_yn  in patches.retired_yn%type DEFAULT NULL,
             i_rerunnable_yn  in patches.rerunnable_yn%type DEFAULT NULL,
             i_warning_count  in patches.warning_count%type DEFAULT NULL,
             i_error_count  in patches.error_count%type DEFAULT NULL,
             i_username  in patches.username%type DEFAULT NULL,
             i_install_log  in patches.install_log%type DEFAULT NULL,
             i_created_by  in patches.created_by%type DEFAULT NULL,
             i_created_on  in patches.created_on%type DEFAULT NULL,
             i_last_updated_by  in patches.last_updated_by%type DEFAULT NULL,
             i_last_updated_on  in patches.last_updated_on%type DEFAULT NULL,
             i_patch_type  in patches.patch_type%type DEFAULT NULL
);


-----------------------------------------------------------------
-- upd_patches_uk1 - use uk to update itself
-----------------------------------------------------------------
PROCEDURE upd_patches_uk1 (
   i_old_patch_name IN patches.patch_name%TYPE
,
   i_new_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   );
------------------------------------------------------------------------------
-- DELETE
------------------------------------------------------------------------------

-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record - using record type
-----------------------------------------------------------------


PROCEDURE del(
    i_patches  in patches%rowtype ) ;
-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record
--   using components, all optional
--   by pk if given, otherwise by uk1
-----------------------------------------------------------------

PROCEDURE del(
             i_patch_id  in patches.patch_id%type default null,
             i_patch_name  in patches.patch_name%type default null,
             i_db_schema  in patches.db_schema%type default null,
             i_branch_name  in patches.branch_name%type default null,
             i_tag_from  in patches.tag_from%type default null,
             i_tag_to  in patches.tag_to%type default null,
             i_supplementary  in patches.supplementary%type default null,
             i_patch_desc  in patches.patch_desc%type default null,
             i_patch_componants  in patches.patch_componants%type default null,
             i_patch_create_date  in patches.patch_create_date%type default null,
             i_patch_created_by  in patches.patch_created_by%type default null,
             i_note  in patches.note%type default null,
             i_log_datetime  in patches.log_datetime%type default null,
             i_completed_datetime  in patches.completed_datetime%type default null,
             i_success_yn  in patches.success_yn%type default null,
             i_retired_yn  in patches.retired_yn%type default null,
             i_rerunnable_yn  in patches.rerunnable_yn%type default null,
             i_warning_count  in patches.warning_count%type default null,
             i_error_count  in patches.error_count%type default null,
             i_username  in patches.username%type default null,
             i_install_log  in patches.install_log%type default null,
             i_created_by  in patches.created_by%type default null,
             i_created_on  in patches.created_on%type default null,
             i_last_updated_by  in patches.last_updated_by%type default null,
             i_last_updated_on  in patches.last_updated_on%type default null,
             i_patch_type  in patches.patch_type%type default null
            ,i_raise_error IN VARCHAR2 DEFAULT 'N'
) ;


------------------------------------------------------------------------------
-- INSERT OR UPDATE
------------------------------------------------------------------------------
-----------------------------------------------------------------
-- ins_upd
-----------------------------------------------------------------
-- insert or update a record using record type, returning record
-- insert a record - if possible
-- update a record - by pk if given, otherwise by uk1
-----------------------------------------------------------------
PROCEDURE ins_upd(
    io_patches  in out patches%rowtype ) ;
-----------------------------------------------------------------
-- ins_upd
-----------------------------------------------------------------
-- insert or update a record using components, returning components
-- insert a record - if possible
-- update a record - by pk if given, otherwise by uk1
-----------------------------------------------------------------

PROCEDURE ins_upd(
             io_patch_id  in out patches.patch_id%type,
             io_patch_name  in out patches.patch_name%type,
             io_db_schema  in out patches.db_schema%type,
             io_branch_name  in out patches.branch_name%type,
             io_tag_from  in out patches.tag_from%type,
             io_tag_to  in out patches.tag_to%type,
             io_supplementary  in out patches.supplementary%type,
             io_patch_desc  in out patches.patch_desc%type,
             io_patch_componants  in out patches.patch_componants%type,
             io_patch_create_date  in out patches.patch_create_date%type,
             io_patch_created_by  in out patches.patch_created_by%type,
             io_note  in out patches.note%type,
             io_log_datetime  in out patches.log_datetime%type,
             io_completed_datetime  in out patches.completed_datetime%type,
             io_success_yn  in out patches.success_yn%type,
             io_retired_yn  in out patches.retired_yn%type,
             io_rerunnable_yn  in out patches.rerunnable_yn%type,
             io_warning_count  in out patches.warning_count%type,
             io_error_count  in out patches.error_count%type,
             io_username  in out patches.username%type,
             io_install_log  in out patches.install_log%type,
             io_created_by  in out patches.created_by%type,
             io_created_on  in out patches.created_on%type,
             io_last_updated_by  in out patches.last_updated_by%type,
             io_last_updated_on  in out patches.last_updated_on%type,
             io_patch_type  in out patches.patch_type%type
) ;

-----------------------------------------------------------------
-- ins_upd
-----------------------------------------------------------------
-- insert or update a record using components, all optional
-- insert a record - if possible
-- update a record - by pk if given, otherwise by uk1, null values ignored.
-----------------------------------------------------------------

PROCEDURE ins_upd_not_null(
             i_patch_id  in patches.patch_id%type default null,
             i_patch_name  in patches.patch_name%type default null,
             i_db_schema  in patches.db_schema%type default null,
             i_branch_name  in patches.branch_name%type default null,
             i_tag_from  in patches.tag_from%type default null,
             i_tag_to  in patches.tag_to%type default null,
             i_supplementary  in patches.supplementary%type default null,
             i_patch_desc  in patches.patch_desc%type default null,
             i_patch_componants  in patches.patch_componants%type default null,
             i_patch_create_date  in patches.patch_create_date%type default null,
             i_patch_created_by  in patches.patch_created_by%type default null,
             i_note  in patches.note%type default null,
             i_log_datetime  in patches.log_datetime%type default null,
             i_completed_datetime  in patches.completed_datetime%type default null,
             i_success_yn  in patches.success_yn%type default null,
             i_retired_yn  in patches.retired_yn%type default null,
             i_rerunnable_yn  in patches.rerunnable_yn%type default null,
             i_warning_count  in patches.warning_count%type default null,
             i_error_count  in patches.error_count%type default null,
             i_username  in patches.username%type default null,
             i_install_log  in patches.install_log%type default null,
             i_created_by  in patches.created_by%type default null,
             i_created_on  in patches.created_on%type default null,
             i_last_updated_by  in patches.last_updated_by%type default null,
             i_last_updated_on  in patches.last_updated_on%type default null,
             i_patch_type  in patches.patch_type%type default null
);

------------------------------------------------------------------------------
-- COLLECTION FUNCTIONS
------------------------------------------------------------------------------
-----------------------------------------------------------------
-- collections_equal
-----------------------------------------------------------------
-- Return true if the the contents of the two collections are the same.
-- In this variant, the collection is based on the rowtype of the
-- the table PATCHES,
-- i_collection1    - first collection for comparison
-- i_collection2    - second collection for comparison
-- i_match_indexes  - if TRUE, then the row numbers in the two
--                         collections must also match.
-- i_both_null_true - if TRUE, then if values in corresponding rows
--                         of both collections are NULL, treat this as equality.
-----------------------------------------------------------------

FUNCTION collections_equal (
      i_collection1     IN   patches_aat
    , i_collection2     IN   patches_aat
    , i_match_indexes   IN   BOOLEAN DEFAULT TRUE
    , i_both_null_true  IN   BOOLEAN DEFAULT TRUE
   )
      RETURN BOOLEAN;


END patches_tapi;
/
