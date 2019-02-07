
  CREATE OR REPLACE EDITIONABLE PACKAGE "PATCHES_TAPI" IS

----------------------------------------------------------------
-- Used by COLLECTION FUNCTIONS
----------------------------------------------------------------

TYPE patches_tab IS TABLE OF patches%ROWTYPE
   INDEX BY BINARY_INTEGER;


-----------------------------------------------------------------
-- RECORD FUNCTIONS
-----------------------------------------------------------------

-----------------------------------------------------------------
-- patches_rid - one row from rowid
-----------------------------------------------------------------
FUNCTION patches_rid (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches%ROWTYPE ;


-----------------------------------------------------------------
-- patches_pk - one row from primary key
-----------------------------------------------------------------
FUNCTION patches_pk (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches%ROWTYPE ;


-----------------------------------------------------------------
-- patches_uk1 - one row from unique index
-----------------------------------------------------------------
FUNCTION patches_uk1 (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches%ROWTYPE ;
-----------------------------------------------------------------
-- patches_uk2 - one row from unique index
-----------------------------------------------------------------
FUNCTION patches_uk2 (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches%ROWTYPE ;


-----------------------------------------------------------------
-- COLUMN FUNCTIONS
-----------------------------------------------------------------

-----------------------------------------------------------------
-- get_rowid - rowid from primary key
-----------------------------------------------------------------
FUNCTION get_rowid (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN rowid ;

-----------------------------------------------------------------
-- get_rowid - rowid from unique index patches_uk1
-----------------------------------------------------------------
FUNCTION get_rowid (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN rowid ;
-----------------------------------------------------------------
-- get_rowid - rowid from unique index patches_uk2
-----------------------------------------------------------------
FUNCTION get_rowid (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN rowid ;



-----------------------------------------------------------------
-- patch_id - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_id (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_id%TYPE;

-----------------------------------------------------------------
-- patch_id - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_id (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_id%TYPE;


-----------------------------------------------------------------
-- patch_id - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_id (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_id%TYPE;


-----------------------------------------------------------------
-- patch_id - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_id (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_id%TYPE;


-----------------------------------------------------------------
-- patch_name - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_name (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_name%TYPE;

-----------------------------------------------------------------
-- patch_name - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_name%TYPE;


-----------------------------------------------------------------
-- patch_name - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_name%TYPE;


-----------------------------------------------------------------
-- patch_name - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_name (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_name%TYPE;


-----------------------------------------------------------------
-- db_schema - retrieved via rowid
-----------------------------------------------------------------
FUNCTION db_schema (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.db_schema%TYPE;

-----------------------------------------------------------------
-- db_schema - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION db_schema (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.db_schema%TYPE;


-----------------------------------------------------------------
-- db_schema - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION db_schema (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.db_schema%TYPE;


-----------------------------------------------------------------
-- db_schema - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION db_schema (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.db_schema%TYPE;


-----------------------------------------------------------------
-- branch_name - retrieved via rowid
-----------------------------------------------------------------
FUNCTION branch_name (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.branch_name%TYPE;

-----------------------------------------------------------------
-- branch_name - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION branch_name (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.branch_name%TYPE;


-----------------------------------------------------------------
-- branch_name - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION branch_name (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.branch_name%TYPE;


-----------------------------------------------------------------
-- branch_name - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION branch_name (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.branch_name%TYPE;


-----------------------------------------------------------------
-- tag_from - retrieved via rowid
-----------------------------------------------------------------
FUNCTION tag_from (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_from%TYPE;

-----------------------------------------------------------------
-- tag_from - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tag_from (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_from%TYPE;


-----------------------------------------------------------------
-- tag_from - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tag_from (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_from%TYPE;


-----------------------------------------------------------------
-- tag_from - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION tag_from (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_from%TYPE;


-----------------------------------------------------------------
-- tag_to - retrieved via rowid
-----------------------------------------------------------------
FUNCTION tag_to (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_to%TYPE;

-----------------------------------------------------------------
-- tag_to - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tag_to (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_to%TYPE;


-----------------------------------------------------------------
-- tag_to - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tag_to (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_to%TYPE;


-----------------------------------------------------------------
-- tag_to - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION tag_to (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_to%TYPE;


-----------------------------------------------------------------
-- supplementary - retrieved via rowid
-----------------------------------------------------------------
FUNCTION supplementary (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.supplementary%TYPE;

-----------------------------------------------------------------
-- supplementary - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION supplementary (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.supplementary%TYPE;


-----------------------------------------------------------------
-- supplementary - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION supplementary (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.supplementary%TYPE;


-----------------------------------------------------------------
-- supplementary - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION supplementary (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.supplementary%TYPE;


-----------------------------------------------------------------
-- patch_desc - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_desc%TYPE;

-----------------------------------------------------------------
-- patch_desc - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_desc%TYPE;


-----------------------------------------------------------------
-- patch_desc - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_desc%TYPE;


-----------------------------------------------------------------
-- patch_desc - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_desc%TYPE;


-----------------------------------------------------------------
-- patch_componants - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_componants%TYPE;

-----------------------------------------------------------------
-- patch_componants - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_componants%TYPE;


-----------------------------------------------------------------
-- patch_componants - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_componants%TYPE;


-----------------------------------------------------------------
-- patch_componants - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_componants%TYPE;


-----------------------------------------------------------------
-- patch_create_date - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_create_date%TYPE;

-----------------------------------------------------------------
-- patch_create_date - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_create_date%TYPE;


-----------------------------------------------------------------
-- patch_create_date - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_create_date%TYPE;


-----------------------------------------------------------------
-- patch_create_date - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_create_date%TYPE;


-----------------------------------------------------------------
-- patch_created_by - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_created_by%TYPE;

-----------------------------------------------------------------
-- patch_created_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_created_by%TYPE;


-----------------------------------------------------------------
-- patch_created_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_created_by%TYPE;


-----------------------------------------------------------------
-- patch_created_by - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_created_by%TYPE;


-----------------------------------------------------------------
-- note - retrieved via rowid
-----------------------------------------------------------------
FUNCTION note (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.note%TYPE;

-----------------------------------------------------------------
-- note - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION note (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.note%TYPE;


-----------------------------------------------------------------
-- note - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION note (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.note%TYPE;


-----------------------------------------------------------------
-- note - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION note (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.note%TYPE;


-----------------------------------------------------------------
-- log_datetime - retrieved via rowid
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.log_datetime%TYPE;

-----------------------------------------------------------------
-- log_datetime - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.log_datetime%TYPE;


-----------------------------------------------------------------
-- log_datetime - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.log_datetime%TYPE;


-----------------------------------------------------------------
-- log_datetime - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.log_datetime%TYPE;


-----------------------------------------------------------------
-- completed_datetime - retrieved via rowid
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.completed_datetime%TYPE;

-----------------------------------------------------------------
-- completed_datetime - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.completed_datetime%TYPE;


-----------------------------------------------------------------
-- completed_datetime - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.completed_datetime%TYPE;


-----------------------------------------------------------------
-- completed_datetime - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.completed_datetime%TYPE;


-----------------------------------------------------------------
-- success_yn - retrieved via rowid
-----------------------------------------------------------------
FUNCTION success_yn (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.success_yn%TYPE;

-----------------------------------------------------------------
-- success_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION success_yn (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.success_yn%TYPE;


-----------------------------------------------------------------
-- success_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION success_yn (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.success_yn%TYPE;


-----------------------------------------------------------------
-- success_yn - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION success_yn (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.success_yn%TYPE;


-----------------------------------------------------------------
-- retired_yn - retrieved via rowid
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.retired_yn%TYPE;

-----------------------------------------------------------------
-- retired_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.retired_yn%TYPE;


-----------------------------------------------------------------
-- retired_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.retired_yn%TYPE;


-----------------------------------------------------------------
-- retired_yn - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.retired_yn%TYPE;


-----------------------------------------------------------------
-- rerunnable_yn - retrieved via rowid
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.rerunnable_yn%TYPE;

-----------------------------------------------------------------
-- rerunnable_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.rerunnable_yn%TYPE;


-----------------------------------------------------------------
-- rerunnable_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.rerunnable_yn%TYPE;


-----------------------------------------------------------------
-- rerunnable_yn - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.rerunnable_yn%TYPE;


-----------------------------------------------------------------
-- warning_count - retrieved via rowid
-----------------------------------------------------------------
FUNCTION warning_count (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.warning_count%TYPE;

-----------------------------------------------------------------
-- warning_count - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION warning_count (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.warning_count%TYPE;


-----------------------------------------------------------------
-- warning_count - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION warning_count (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.warning_count%TYPE;


-----------------------------------------------------------------
-- warning_count - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION warning_count (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.warning_count%TYPE;


-----------------------------------------------------------------
-- error_count - retrieved via rowid
-----------------------------------------------------------------
FUNCTION error_count (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.error_count%TYPE;

-----------------------------------------------------------------
-- error_count - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION error_count (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.error_count%TYPE;


-----------------------------------------------------------------
-- error_count - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION error_count (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.error_count%TYPE;


-----------------------------------------------------------------
-- error_count - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION error_count (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.error_count%TYPE;


-----------------------------------------------------------------
-- username - retrieved via rowid
-----------------------------------------------------------------
FUNCTION username (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.username%TYPE;

-----------------------------------------------------------------
-- username - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION username (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.username%TYPE;


-----------------------------------------------------------------
-- username - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION username (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.username%TYPE;


-----------------------------------------------------------------
-- username - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION username (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.username%TYPE;


-----------------------------------------------------------------
-- install_log - retrieved via rowid
-----------------------------------------------------------------
FUNCTION install_log (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.install_log%TYPE;

-----------------------------------------------------------------
-- install_log - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION install_log (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.install_log%TYPE;


-----------------------------------------------------------------
-- install_log - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION install_log (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.install_log%TYPE;


-----------------------------------------------------------------
-- install_log - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION install_log (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.install_log%TYPE;


-----------------------------------------------------------------
-- created_by - retrieved via rowid
-----------------------------------------------------------------
FUNCTION created_by (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_by%TYPE;

-----------------------------------------------------------------
-- created_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_by%TYPE;


-----------------------------------------------------------------
-- created_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_by%TYPE;


-----------------------------------------------------------------
-- created_by - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION created_by (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_by%TYPE;


-----------------------------------------------------------------
-- created_on - retrieved via rowid
-----------------------------------------------------------------
FUNCTION created_on (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_on%TYPE;

-----------------------------------------------------------------
-- created_on - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_on%TYPE;


-----------------------------------------------------------------
-- created_on - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_on%TYPE;


-----------------------------------------------------------------
-- created_on - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION created_on (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_on%TYPE;


-----------------------------------------------------------------
-- last_updated_by - retrieved via rowid
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_by%TYPE;

-----------------------------------------------------------------
-- last_updated_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_by%TYPE;


-----------------------------------------------------------------
-- last_updated_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_by%TYPE;


-----------------------------------------------------------------
-- last_updated_by - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_by%TYPE;


-----------------------------------------------------------------
-- last_updated_on - retrieved via rowid
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_on%TYPE;

-----------------------------------------------------------------
-- last_updated_on - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_on%TYPE;


-----------------------------------------------------------------
-- last_updated_on - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_on%TYPE;


-----------------------------------------------------------------
-- last_updated_on - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_on%TYPE;


-----------------------------------------------------------------
-- patch_type - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_type (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_type%TYPE;

-----------------------------------------------------------------
-- patch_type - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_type (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_type%TYPE;


-----------------------------------------------------------------
-- patch_type - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_type (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_type%TYPE;


-----------------------------------------------------------------
-- patch_type - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION patch_type (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_type%TYPE;


-----------------------------------------------------------------
-- tracking_yn - retrieved via rowid
-----------------------------------------------------------------
FUNCTION tracking_yn (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tracking_yn%TYPE;

-----------------------------------------------------------------
-- tracking_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tracking_yn (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tracking_yn%TYPE;


-----------------------------------------------------------------
-- tracking_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tracking_yn (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tracking_yn%TYPE;


-----------------------------------------------------------------
-- tracking_yn - retrieved via unique index patches_uk2
-----------------------------------------------------------------
FUNCTION tracking_yn (
   i_db_schema     IN patches.db_schema%TYPE
  ,i_branch_name   IN patches.branch_name%TYPE
  ,i_tag_from      IN patches.tag_from%TYPE
  ,i_tag_to        IN patches.tag_to%TYPE
  ,i_supplementary IN patches.supplementary%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tracking_yn%TYPE;



-----------------------------------------------------------------
-- get_rowid
-----------------------------------------------------------------
-- get the current record rowid by pk if given, otherwise by uk1
-----------------------------------------------------------------

FUNCTION get_rowid(
    i_patches IN patches%rowtype
   ,i_raise_error        IN VARCHAR2 DEFAULT 'N' ) return rowid;

-----------------------------------------------------------------
-- get_current_rec
-----------------------------------------------------------------
-- get the current record by pk if given, otherwise by uk1
-----------------------------------------------------------------

FUNCTION get_current_rec(
             i_patches IN patches%rowtype
            ,i_raise_error IN VARCHAR2 DEFAULT 'Y'
) RETURN patches%rowtype;


-----------------------------------------------------------------
-- create_rec
-----------------------------------------------------------------
-- create a record from its component fields
-----------------------------------------------------------------

FUNCTION create_rec(
     i_patch_id           IN patches.patch_id          %TYPE DEFAULT NULL
    ,i_patch_name         IN patches.patch_name        %TYPE DEFAULT NULL
    ,i_db_schema          IN patches.db_schema         %TYPE DEFAULT NULL
    ,i_branch_name        IN patches.branch_name       %TYPE DEFAULT NULL
    ,i_tag_from           IN patches.tag_from          %TYPE DEFAULT NULL
    ,i_tag_to             IN patches.tag_to            %TYPE DEFAULT NULL
    ,i_supplementary      IN patches.supplementary     %TYPE DEFAULT NULL
    ,i_patch_desc         IN patches.patch_desc        %TYPE DEFAULT NULL
    ,i_patch_componants   IN patches.patch_componants  %TYPE DEFAULT NULL
    ,i_patch_create_date  IN patches.patch_create_date %TYPE DEFAULT NULL
    ,i_patch_created_by   IN patches.patch_created_by  %TYPE DEFAULT NULL
    ,i_note               IN patches.note              %TYPE DEFAULT NULL
    ,i_log_datetime       IN patches.log_datetime      %TYPE DEFAULT NULL
    ,i_completed_datetime IN patches.completed_datetime%TYPE DEFAULT NULL
    ,i_success_yn         IN patches.success_yn        %TYPE DEFAULT NULL
    ,i_retired_yn         IN patches.retired_yn        %TYPE DEFAULT NULL
    ,i_rerunnable_yn      IN patches.rerunnable_yn     %TYPE DEFAULT NULL
    ,i_warning_count      IN patches.warning_count     %TYPE DEFAULT NULL
    ,i_error_count        IN patches.error_count       %TYPE DEFAULT NULL
    ,i_username           IN patches.username          %TYPE DEFAULT NULL
    ,i_install_log        IN patches.install_log       %TYPE DEFAULT NULL
    ,i_created_by         IN patches.created_by        %TYPE DEFAULT NULL
    ,i_created_on         IN patches.created_on        %TYPE DEFAULT NULL
    ,i_last_updated_by    IN patches.last_updated_by   %TYPE DEFAULT NULL
    ,i_last_updated_on    IN patches.last_updated_on   %TYPE DEFAULT NULL
    ,i_patch_type         IN patches.patch_type        %TYPE DEFAULT NULL
    ,i_tracking_yn        IN patches.tracking_yn       %TYPE DEFAULT NULL
) RETURN patches%ROWTYPE;

-----------------------------------------------------------------
-- split_rec
-----------------------------------------------------------------
-- split a record into its component fields
-----------------------------------------------------------------

PROCEDURE split_rec( i_patches in patches%rowtype
                    ,o_patch_id           OUT patches.patch_id          %TYPE
                    ,o_patch_name         OUT patches.patch_name        %TYPE
                    ,o_db_schema          OUT patches.db_schema         %TYPE
                    ,o_branch_name        OUT patches.branch_name       %TYPE
                    ,o_tag_from           OUT patches.tag_from          %TYPE
                    ,o_tag_to             OUT patches.tag_to            %TYPE
                    ,o_supplementary      OUT patches.supplementary     %TYPE
                    ,o_patch_desc         OUT patches.patch_desc        %TYPE
                    ,o_patch_componants   OUT patches.patch_componants  %TYPE
                    ,o_patch_create_date  OUT patches.patch_create_date %TYPE
                    ,o_patch_created_by   OUT patches.patch_created_by  %TYPE
                    ,o_note               OUT patches.note              %TYPE
                    ,o_log_datetime       OUT patches.log_datetime      %TYPE
                    ,o_completed_datetime OUT patches.completed_datetime%TYPE
                    ,o_success_yn         OUT patches.success_yn        %TYPE
                    ,o_retired_yn         OUT patches.retired_yn        %TYPE
                    ,o_rerunnable_yn      OUT patches.rerunnable_yn     %TYPE
                    ,o_warning_count      OUT patches.warning_count     %TYPE
                    ,o_error_count        OUT patches.error_count       %TYPE
                    ,o_username           OUT patches.username          %TYPE
                    ,o_install_log        OUT patches.install_log       %TYPE
                    ,o_created_by         OUT patches.created_by        %TYPE
                    ,o_created_on         OUT patches.created_on        %TYPE
                    ,o_last_updated_by    OUT patches.last_updated_by   %TYPE
                    ,o_last_updated_on    OUT patches.last_updated_on   %TYPE
                    ,o_patch_type         OUT patches.patch_type        %TYPE
                    ,o_tracking_yn        OUT patches.tracking_yn       %TYPE
);

-----------------------------------------------------------------
-- merge_old_and_new
-----------------------------------------------------------------
-- null values in NEW replaced with values from OLD
-----------------------------------------------------------------

PROCEDURE merge_old_and_new(i_old_rec  IN     patches%rowtype
                           ,io_new_rec IN OUT patches%rowtype);


-----------------------------------------------------------------
-- merge_old_and_new
-----------------------------------------------------------------
-- New rec will include new values of fields listed in i_merge_column_list,
-- all other fields will keep the old values.
-- + For every field NOT listed, Replace that value in NEW with orig value from OLD.
-----------------------------------------------------------------

PROCEDURE merge_old_and_new(i_old_rec        IN     patches%rowtype
                           ,io_new_rec       IN OUT patches%rowtype
                           ,i_merge_col_list in     varchar2);

------------------------------------------------------------------------------
-- INSERT
------------------------------------------------------------------------------

-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using record type, returning record
--   uses returning clause.
--   SUITABLE for tables with or without long columns
-----------------------------------------------------------------

PROCEDURE ins(
    io_patches  in out patches%rowtype
   ,io_rowid             in out rowid );

-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using record type, returning record
--   uses returning clause.
--   Suitable for tables with or without long columns
--   Does not return rowid.
-----------------------------------------------------------------

PROCEDURE ins(
    io_patches  in out patches%rowtype );


-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using components, returning components
-- Does not return rowid.
-----------------------------------------------------------------


PROCEDURE ins(
     io_patch_id           IN OUT patches.patch_id%TYPE
    ,io_patch_name         IN OUT patches.patch_name%TYPE
    ,io_db_schema          IN OUT patches.db_schema%TYPE
    ,io_branch_name        IN OUT patches.branch_name%TYPE
    ,io_tag_from           IN OUT patches.tag_from%TYPE
    ,io_tag_to             IN OUT patches.tag_to%TYPE
    ,io_supplementary      IN OUT patches.supplementary%TYPE
    ,io_patch_desc         IN OUT patches.patch_desc%TYPE
    ,io_patch_componants   IN OUT patches.patch_componants%TYPE
    ,io_patch_create_date  IN OUT patches.patch_create_date%TYPE
    ,io_patch_created_by   IN OUT patches.patch_created_by%TYPE
    ,io_note               IN OUT patches.note%TYPE
    ,io_log_datetime       IN OUT patches.log_datetime%TYPE
    ,io_completed_datetime IN OUT patches.completed_datetime%TYPE
    ,io_success_yn         IN OUT patches.success_yn%TYPE
    ,io_retired_yn         IN OUT patches.retired_yn%TYPE
    ,io_rerunnable_yn      IN OUT patches.rerunnable_yn%TYPE
    ,io_warning_count      IN OUT patches.warning_count%TYPE
    ,io_error_count        IN OUT patches.error_count%TYPE
    ,io_username           IN OUT patches.username%TYPE
    ,io_install_log        IN OUT patches.install_log%TYPE
    ,io_created_by         IN OUT patches.created_by%TYPE
    ,io_created_on         IN OUT patches.created_on%TYPE
    ,io_last_updated_by    IN OUT patches.last_updated_by%TYPE
    ,io_last_updated_on    IN OUT patches.last_updated_on%TYPE
    ,io_patch_type         IN OUT patches.patch_type%TYPE
    ,io_tracking_yn        IN OUT patches.tracking_yn%TYPE
);

-----------------------------------------------------------------
-- ins_opt
-----------------------------------------------------------------
-- insert a record - using components, all optional
-----------------------------------------------------------------
PROCEDURE ins_opt(
     i_patch_id           IN patches.patch_id          %TYPE DEFAULT NULL
    ,i_patch_name         IN patches.patch_name        %TYPE DEFAULT NULL
    ,i_db_schema          IN patches.db_schema         %TYPE DEFAULT NULL
    ,i_branch_name        IN patches.branch_name       %TYPE DEFAULT NULL
    ,i_tag_from           IN patches.tag_from          %TYPE DEFAULT NULL
    ,i_tag_to             IN patches.tag_to            %TYPE DEFAULT NULL
    ,i_supplementary      IN patches.supplementary     %TYPE DEFAULT NULL
    ,i_patch_desc         IN patches.patch_desc        %TYPE DEFAULT NULL
    ,i_patch_componants   IN patches.patch_componants  %TYPE DEFAULT NULL
    ,i_patch_create_date  IN patches.patch_create_date %TYPE DEFAULT NULL
    ,i_patch_created_by   IN patches.patch_created_by  %TYPE DEFAULT NULL
    ,i_note               IN patches.note              %TYPE DEFAULT NULL
    ,i_log_datetime       IN patches.log_datetime      %TYPE DEFAULT NULL
    ,i_completed_datetime IN patches.completed_datetime%TYPE DEFAULT NULL
    ,i_success_yn         IN patches.success_yn        %TYPE DEFAULT NULL
    ,i_retired_yn         IN patches.retired_yn        %TYPE DEFAULT NULL
    ,i_rerunnable_yn      IN patches.rerunnable_yn     %TYPE DEFAULT NULL
    ,i_warning_count      IN patches.warning_count     %TYPE DEFAULT NULL
    ,i_error_count        IN patches.error_count       %TYPE DEFAULT NULL
    ,i_username           IN patches.username          %TYPE DEFAULT NULL
    ,i_install_log        IN patches.install_log       %TYPE DEFAULT NULL
    ,i_created_by         IN patches.created_by        %TYPE DEFAULT NULL
    ,i_created_on         IN patches.created_on        %TYPE DEFAULT NULL
    ,i_last_updated_by    IN patches.last_updated_by   %TYPE DEFAULT NULL
    ,i_last_updated_on    IN patches.last_updated_on   %TYPE DEFAULT NULL
    ,i_patch_type         IN patches.patch_type        %TYPE DEFAULT NULL
    ,i_tracking_yn        IN patches.tracking_yn       %TYPE DEFAULT NULL
);

------------------------------------------------------------------------------
-- UPDATE
------------------------------------------------------------------------------

-----------------------------------------------------------------
-- upd
-----------------------------------------------------------------
-- update a record - using record type, primary key cols, returning record
--   uses returning clause.
--   Suitable for tables with or without long columns
-----------------------------------------------------------------

PROCEDURE upd(
    io_patches  in out patches%rowtype
   ,io_rowid             in out rowid );


-----------------------------------------------------------------
-- upd
-----------------------------------------------------------------
-- update a record - using record type, primary key cols, returning record
--   uses returning clause.
--   Suitable for tables with or without long columns
--   Does not return rowid.
-----------------------------------------------------------------

PROCEDURE upd(
    io_patches  in out patches%rowtype );


-----------------------------------------------------------------
-- upd
-----------------------------------------------------------------
-- update a record - using components, returning components
-- Does not return rowid.
-----------------------------------------------------------------

PROCEDURE upd(
     io_patch_id           IN OUT patches.patch_id%TYPE
    ,io_patch_name         IN OUT patches.patch_name%TYPE
    ,io_db_schema          IN OUT patches.db_schema%TYPE
    ,io_branch_name        IN OUT patches.branch_name%TYPE
    ,io_tag_from           IN OUT patches.tag_from%TYPE
    ,io_tag_to             IN OUT patches.tag_to%TYPE
    ,io_supplementary      IN OUT patches.supplementary%TYPE
    ,io_patch_desc         IN OUT patches.patch_desc%TYPE
    ,io_patch_componants   IN OUT patches.patch_componants%TYPE
    ,io_patch_create_date  IN OUT patches.patch_create_date%TYPE
    ,io_patch_created_by   IN OUT patches.patch_created_by%TYPE
    ,io_note               IN OUT patches.note%TYPE
    ,io_log_datetime       IN OUT patches.log_datetime%TYPE
    ,io_completed_datetime IN OUT patches.completed_datetime%TYPE
    ,io_success_yn         IN OUT patches.success_yn%TYPE
    ,io_retired_yn         IN OUT patches.retired_yn%TYPE
    ,io_rerunnable_yn      IN OUT patches.rerunnable_yn%TYPE
    ,io_warning_count      IN OUT patches.warning_count%TYPE
    ,io_error_count        IN OUT patches.error_count%TYPE
    ,io_username           IN OUT patches.username%TYPE
    ,io_install_log        IN OUT patches.install_log%TYPE
    ,io_created_by         IN OUT patches.created_by%TYPE
    ,io_created_on         IN OUT patches.created_on%TYPE
    ,io_last_updated_by    IN OUT patches.last_updated_by%TYPE
    ,io_last_updated_on    IN OUT patches.last_updated_on%TYPE
    ,io_patch_type         IN OUT patches.patch_type%TYPE
    ,io_tracking_yn        IN OUT patches.tracking_yn%TYPE
);

-------------------------------------------------------------------------
-- upd_not_null
-------------------------------------------------------------------------
-- update a record
--   using components, all optional
--   by pk if given, otherwise by uk1, null values ignored.
-----------------------------------------------------------------
PROCEDURE upd_not_null(
     i_patch_id           IN patches.patch_id%TYPE DEFAULT NULL
    ,i_patch_name         IN patches.patch_name%TYPE DEFAULT NULL
    ,i_db_schema          IN patches.db_schema%TYPE DEFAULT NULL
    ,i_branch_name        IN patches.branch_name%TYPE DEFAULT NULL
    ,i_tag_from           IN patches.tag_from%TYPE DEFAULT NULL
    ,i_tag_to             IN patches.tag_to%TYPE DEFAULT NULL
    ,i_supplementary      IN patches.supplementary%TYPE DEFAULT NULL
    ,i_patch_desc         IN patches.patch_desc%TYPE DEFAULT NULL
    ,i_patch_componants   IN patches.patch_componants%TYPE DEFAULT NULL
    ,i_patch_create_date  IN patches.patch_create_date%TYPE DEFAULT NULL
    ,i_patch_created_by   IN patches.patch_created_by%TYPE DEFAULT NULL
    ,i_note               IN patches.note%TYPE DEFAULT NULL
    ,i_log_datetime       IN patches.log_datetime%TYPE DEFAULT NULL
    ,i_completed_datetime IN patches.completed_datetime%TYPE DEFAULT NULL
    ,i_success_yn         IN patches.success_yn%TYPE DEFAULT NULL
    ,i_retired_yn         IN patches.retired_yn%TYPE DEFAULT NULL
    ,i_rerunnable_yn      IN patches.rerunnable_yn%TYPE DEFAULT NULL
    ,i_warning_count      IN patches.warning_count%TYPE DEFAULT NULL
    ,i_error_count        IN patches.error_count%TYPE DEFAULT NULL
    ,i_username           IN patches.username%TYPE DEFAULT NULL
    ,i_install_log        IN patches.install_log%TYPE DEFAULT NULL
    ,i_created_by         IN patches.created_by%TYPE DEFAULT NULL
    ,i_created_on         IN patches.created_on%TYPE DEFAULT NULL
    ,i_last_updated_by    IN patches.last_updated_by%TYPE DEFAULT NULL
    ,i_last_updated_on    IN patches.last_updated_on%TYPE DEFAULT NULL
    ,i_patch_type         IN patches.patch_type%TYPE DEFAULT NULL
    ,i_tracking_yn        IN patches.tracking_yn%TYPE DEFAULT NULL
    ,i_raise_error         IN VARCHAR2 DEFAULT 'N'
);


-------------------------------------------------------------------------
-- upd_opt
-------------------------------------------------------------------------
-- update a record
--   using components, all optional
--   by pk if given, otherwise by uk1
--   Updates only columns listed in i_upd_col_list.
-----------------------------------------------------------------
PROCEDURE upd_opt(
     i_patch_id           IN patches.patch_id%TYPE DEFAULT NULL
    ,i_patch_name         IN patches.patch_name%TYPE DEFAULT NULL
    ,i_db_schema          IN patches.db_schema%TYPE DEFAULT NULL
    ,i_branch_name        IN patches.branch_name%TYPE DEFAULT NULL
    ,i_tag_from           IN patches.tag_from%TYPE DEFAULT NULL
    ,i_tag_to             IN patches.tag_to%TYPE DEFAULT NULL
    ,i_supplementary      IN patches.supplementary%TYPE DEFAULT NULL
    ,i_patch_desc         IN patches.patch_desc%TYPE DEFAULT NULL
    ,i_patch_componants   IN patches.patch_componants%TYPE DEFAULT NULL
    ,i_patch_create_date  IN patches.patch_create_date%TYPE DEFAULT NULL
    ,i_patch_created_by   IN patches.patch_created_by%TYPE DEFAULT NULL
    ,i_note               IN patches.note%TYPE DEFAULT NULL
    ,i_log_datetime       IN patches.log_datetime%TYPE DEFAULT NULL
    ,i_completed_datetime IN patches.completed_datetime%TYPE DEFAULT NULL
    ,i_success_yn         IN patches.success_yn%TYPE DEFAULT NULL
    ,i_retired_yn         IN patches.retired_yn%TYPE DEFAULT NULL
    ,i_rerunnable_yn      IN patches.rerunnable_yn%TYPE DEFAULT NULL
    ,i_warning_count      IN patches.warning_count%TYPE DEFAULT NULL
    ,i_error_count        IN patches.error_count%TYPE DEFAULT NULL
    ,i_username           IN patches.username%TYPE DEFAULT NULL
    ,i_install_log        IN patches.install_log%TYPE DEFAULT NULL
    ,i_created_by         IN patches.created_by%TYPE DEFAULT NULL
    ,i_created_on         IN patches.created_on%TYPE DEFAULT NULL
    ,i_last_updated_by    IN patches.last_updated_by%TYPE DEFAULT NULL
    ,i_last_updated_on    IN patches.last_updated_on%TYPE DEFAULT NULL
    ,i_patch_type         IN patches.patch_type%TYPE DEFAULT NULL
    ,i_tracking_yn        IN patches.tracking_yn%TYPE DEFAULT NULL
    ,i_upd_col_list        IN varchar2
    ,i_raise_error         IN VARCHAR2 DEFAULT 'N'
);

-----------------------------------------------------------------
-- upd_patches_uk1 - use uk to update itself
-----------------------------------------------------------------

PROCEDURE upd_patches_uk1 (
     i_old_patch_name IN patches.patch_name%TYPE
      ,i_new_patch_name IN patches.patch_name%TYPE

  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   );

-----------------------------------------------------------------
-- upd_patches_uk2 - use uk to update itself
-----------------------------------------------------------------

PROCEDURE upd_patches_uk2 (
     i_old_db_schema     IN patches.db_schema%TYPE
    ,i_old_branch_name   IN patches.branch_name%TYPE
    ,i_old_tag_from      IN patches.tag_from%TYPE
    ,i_old_tag_to        IN patches.tag_to%TYPE
    ,i_old_supplementary IN patches.supplementary%TYPE
      ,i_new_db_schema     IN patches.db_schema%TYPE

    ,i_new_branch_name   IN patches.branch_name%TYPE

    ,i_new_tag_from      IN patches.tag_from%TYPE

    ,i_new_tag_to        IN patches.tag_to%TYPE

    ,i_new_supplementary IN patches.supplementary%TYPE

  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   );



------------------------------------------------------------------------------
-- DELETE
------------------------------------------------------------------------------

-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record - using rowid
-----------------------------------------------------------------


PROCEDURE del(
    i_rowid        IN rowid
   ,i_raise_error  IN VARCHAR2 DEFAULT 'N' );


-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record - using record type
-----------------------------------------------------------------


PROCEDURE del(
    i_patches  in patches%rowtype
   ,i_raise_error        IN VARCHAR2 DEFAULT 'N' );



-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record
--   using components, all optional
--   by pk if given, otherwise by uk1
-----------------------------------------------------------------

PROCEDURE del(
     i_patch_id           IN patches.patch_id%TYPE DEFAULT NULL
    ,i_patch_name         IN patches.patch_name%TYPE DEFAULT NULL
    ,i_db_schema          IN patches.db_schema%TYPE DEFAULT NULL
    ,i_branch_name        IN patches.branch_name%TYPE DEFAULT NULL
    ,i_tag_from           IN patches.tag_from%TYPE DEFAULT NULL
    ,i_tag_to             IN patches.tag_to%TYPE DEFAULT NULL
    ,i_supplementary      IN patches.supplementary%TYPE DEFAULT NULL
    ,i_patch_desc         IN patches.patch_desc%TYPE DEFAULT NULL
    ,i_patch_componants   IN patches.patch_componants%TYPE DEFAULT NULL
    ,i_patch_create_date  IN patches.patch_create_date%TYPE DEFAULT NULL
    ,i_patch_created_by   IN patches.patch_created_by%TYPE DEFAULT NULL
    ,i_note               IN patches.note%TYPE DEFAULT NULL
    ,i_log_datetime       IN patches.log_datetime%TYPE DEFAULT NULL
    ,i_completed_datetime IN patches.completed_datetime%TYPE DEFAULT NULL
    ,i_success_yn         IN patches.success_yn%TYPE DEFAULT NULL
    ,i_retired_yn         IN patches.retired_yn%TYPE DEFAULT NULL
    ,i_rerunnable_yn      IN patches.rerunnable_yn%TYPE DEFAULT NULL
    ,i_warning_count      IN patches.warning_count%TYPE DEFAULT NULL
    ,i_error_count        IN patches.error_count%TYPE DEFAULT NULL
    ,i_username           IN patches.username%TYPE DEFAULT NULL
    ,i_install_log        IN patches.install_log%TYPE DEFAULT NULL
    ,i_created_by         IN patches.created_by%TYPE DEFAULT NULL
    ,i_created_on         IN patches.created_on%TYPE DEFAULT NULL
    ,i_last_updated_by    IN patches.last_updated_by%TYPE DEFAULT NULL
    ,i_last_updated_on    IN patches.last_updated_on%TYPE DEFAULT NULL
    ,i_patch_type         IN patches.patch_type%TYPE DEFAULT NULL
    ,i_tracking_yn        IN patches.tracking_yn%TYPE DEFAULT NULL
    ,i_raise_error IN VARCHAR2 DEFAULT 'N'
);



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
    io_patches  in out patches%rowtype );


-----------------------------------------------------------------
-- ins_upd
-----------------------------------------------------------------
-- insert or update a record using components, returning components
-- insert a record - if possible
-- update a record - by pk if given, otherwise by uk1
-----------------------------------------------------------------

PROCEDURE ins_upd(
     io_patch_id           IN OUT patches.patch_id%TYPE
    ,io_patch_name         IN OUT patches.patch_name%TYPE
    ,io_db_schema          IN OUT patches.db_schema%TYPE
    ,io_branch_name        IN OUT patches.branch_name%TYPE
    ,io_tag_from           IN OUT patches.tag_from%TYPE
    ,io_tag_to             IN OUT patches.tag_to%TYPE
    ,io_supplementary      IN OUT patches.supplementary%TYPE
    ,io_patch_desc         IN OUT patches.patch_desc%TYPE
    ,io_patch_componants   IN OUT patches.patch_componants%TYPE
    ,io_patch_create_date  IN OUT patches.patch_create_date%TYPE
    ,io_patch_created_by   IN OUT patches.patch_created_by%TYPE
    ,io_note               IN OUT patches.note%TYPE
    ,io_log_datetime       IN OUT patches.log_datetime%TYPE
    ,io_completed_datetime IN OUT patches.completed_datetime%TYPE
    ,io_success_yn         IN OUT patches.success_yn%TYPE
    ,io_retired_yn         IN OUT patches.retired_yn%TYPE
    ,io_rerunnable_yn      IN OUT patches.rerunnable_yn%TYPE
    ,io_warning_count      IN OUT patches.warning_count%TYPE
    ,io_error_count        IN OUT patches.error_count%TYPE
    ,io_username           IN OUT patches.username%TYPE
    ,io_install_log        IN OUT patches.install_log%TYPE
    ,io_created_by         IN OUT patches.created_by%TYPE
    ,io_created_on         IN OUT patches.created_on%TYPE
    ,io_last_updated_by    IN OUT patches.last_updated_by%TYPE
    ,io_last_updated_on    IN OUT patches.last_updated_on%TYPE
    ,io_patch_type         IN OUT patches.patch_type%TYPE
    ,io_tracking_yn        IN OUT patches.tracking_yn%TYPE
);

-----------------------------------------------------------------
-- ins_upd_not_null
-----------------------------------------------------------------
-- insert or update a record using components, all optional
-- insert a record - if possible
-- update a record - by pk if given, otherwise by uk1, null values ignored.
-----------------------------------------------------------------

PROCEDURE ins_upd_not_null(
     i_patch_id           IN patches.patch_id%TYPE DEFAULT NULL
    ,i_patch_name         IN patches.patch_name%TYPE DEFAULT NULL
    ,i_db_schema          IN patches.db_schema%TYPE DEFAULT NULL
    ,i_branch_name        IN patches.branch_name%TYPE DEFAULT NULL
    ,i_tag_from           IN patches.tag_from%TYPE DEFAULT NULL
    ,i_tag_to             IN patches.tag_to%TYPE DEFAULT NULL
    ,i_supplementary      IN patches.supplementary%TYPE DEFAULT NULL
    ,i_patch_desc         IN patches.patch_desc%TYPE DEFAULT NULL
    ,i_patch_componants   IN patches.patch_componants%TYPE DEFAULT NULL
    ,i_patch_create_date  IN patches.patch_create_date%TYPE DEFAULT NULL
    ,i_patch_created_by   IN patches.patch_created_by%TYPE DEFAULT NULL
    ,i_note               IN patches.note%TYPE DEFAULT NULL
    ,i_log_datetime       IN patches.log_datetime%TYPE DEFAULT NULL
    ,i_completed_datetime IN patches.completed_datetime%TYPE DEFAULT NULL
    ,i_success_yn         IN patches.success_yn%TYPE DEFAULT NULL
    ,i_retired_yn         IN patches.retired_yn%TYPE DEFAULT NULL
    ,i_rerunnable_yn      IN patches.rerunnable_yn%TYPE DEFAULT NULL
    ,i_warning_count      IN patches.warning_count%TYPE DEFAULT NULL
    ,i_error_count        IN patches.error_count%TYPE DEFAULT NULL
    ,i_username           IN patches.username%TYPE DEFAULT NULL
    ,i_install_log        IN patches.install_log%TYPE DEFAULT NULL
    ,i_created_by         IN patches.created_by%TYPE DEFAULT NULL
    ,i_created_on         IN patches.created_on%TYPE DEFAULT NULL
    ,i_last_updated_by    IN patches.last_updated_by%TYPE DEFAULT NULL
    ,i_last_updated_on    IN patches.last_updated_on%TYPE DEFAULT NULL
    ,i_patch_type         IN patches.patch_type%TYPE DEFAULT NULL
    ,i_tracking_yn        IN patches.tracking_yn%TYPE DEFAULT NULL
);



------------------------------------------------------------------------------
-- DATA UNLOADING
------------------------------------------------------------------------------

------------------------------------------------------------------------------
-- unload_data
------------------------------------------------------------------------------
-- unload data into a script ins_upd statements
------------------------------------------------------------------------------
procedure unload_data;

------------------------------------------------------------------------------
-- COLLECTION FUNCTIONS
------------------------------------------------------------------------------
-----------------------------------------------------------------
-- collections_equal
-----------------------------------------------------------------
-- Return true if the the contents of the two collections are the same.
-- In this variant, the collection is based on the rowtype of the
-- the table patches,
-- i_collection1    - first collection for comparison
-- i_collection2    - second collection for comparison
-- i_match_indexes  - if TRUE, then the row numbers in the two
--                         collections must also match.
-- i_both_null_true - if TRUE, then if values in corresponding rows
--                         of both collections are NULL, treat this as equality.
-----------------------------------------------------------------

FUNCTION collections_equal (
  i_collection1     IN   patches_tab
, i_collection2     IN   patches_tab
, i_match_indexes   IN   BOOLEAN DEFAULT TRUE
, i_both_null_true  IN   BOOLEAN DEFAULT TRUE
)
RETURN BOOLEAN;

END patches_tapi;
/

--GRANTS


--SYNONYMS
