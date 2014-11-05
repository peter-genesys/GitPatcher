CREATE OR REPLACE PACKAGE BODY patches_tapi IS

-----------------------------------------------------------------
-- RECORD FUNCTIONS
-----------------------------------------------------------------

-----------------------------------------------------------------
-- patches_pk - one row from primary key
-----------------------------------------------------------------
FUNCTION patches_pk (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES%ROWTYPE
IS
   CURSOR cr_patches
   IS
      SELECT *
        FROM patches
       WHERE
          PATCH_ID = i_patch_id
      ;
   l_result PATCHES%ROWTYPE;
   l_found   BOOLEAN;
   x_unknown_key EXCEPTION;
BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
     RAISE x_unknown_key;
   END IF;

   RETURN l_result;

EXCEPTION
  WHEN x_unknown_key THEN
    RAISE NO_DATA_FOUND;

END patches_pk;


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
   ) RETURN PATCHES%ROWTYPE
IS
   CURSOR cr_patches IS
      SELECT *
        FROM PATCHES
       WHERE
            (i_db_schema is null or DB_SCHEMA = i_db_schema) AND
            (i_branch_name is null or BRANCH_NAME = i_branch_name) AND
            (i_tag_from is null or TAG_FROM = i_tag_from) AND
            (i_tag_to is null or TAG_TO = i_tag_to) AND
            (i_supplementary is null or SUPPLEMENTARY = i_supplementary)
      ;
   l_result PATCHES%ROWTYPE;
   l_found   BOOLEAN;
   x_unknown_key EXCEPTION;
BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
     RAISE x_unknown_key;
   END IF;

   RETURN l_result;

EXCEPTION
  WHEN x_unknown_key THEN
    RAISE NO_DATA_FOUND;

END patches_uk2;


-----------------------------------------------------------------
-- patches_uk1 - one row from unique index
-----------------------------------------------------------------
FUNCTION patches_uk1 (
   i_patch_name IN PATCHES.PATCH_NAME%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   ) RETURN PATCHES%ROWTYPE
IS
   CURSOR cr_patches IS
      SELECT *
        FROM PATCHES
       WHERE
            (i_patch_name is null or PATCH_NAME = i_patch_name)
      ;
   l_result PATCHES%ROWTYPE;
   l_found   BOOLEAN;
   x_unknown_key EXCEPTION;
BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
     RAISE x_unknown_key;
   END IF;

   RETURN l_result;

EXCEPTION
  WHEN x_unknown_key THEN
    RAISE NO_DATA_FOUND;

END patches_uk1;



-----------------------------------------------------------------
-- COLUMN FUNCTIONS
-----------------------------------------------------------------


-----------------------------------------------------------------
-- patch_name - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_name%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;


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
   )  RETURN PATCHES.patch_name%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;



-----------------------------------------------------------------
-- patch_name - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_name%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;




-----------------------------------------------------------------
-- db_schema - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION db_schema (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.db_schema%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).db_schema;

END db_schema;


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
   )  RETURN PATCHES.db_schema%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).db_schema;

END db_schema;



-----------------------------------------------------------------
-- db_schema - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION db_schema (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.db_schema%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).db_schema;

END db_schema;




-----------------------------------------------------------------
-- branch_name - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION branch_name (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.branch_name%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).branch_name;

END branch_name;


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
   )  RETURN PATCHES.branch_name%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).branch_name;

END branch_name;



-----------------------------------------------------------------
-- branch_name - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION branch_name (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.branch_name%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).branch_name;

END branch_name;




-----------------------------------------------------------------
-- tag_from - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tag_from (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_from%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).tag_from;

END tag_from;


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
   )  RETURN PATCHES.tag_from%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).tag_from;

END tag_from;



-----------------------------------------------------------------
-- tag_from - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tag_from (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_from%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).tag_from;

END tag_from;




-----------------------------------------------------------------
-- tag_to - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tag_to (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_to%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).tag_to;

END tag_to;


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
   )  RETURN PATCHES.tag_to%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).tag_to;

END tag_to;



-----------------------------------------------------------------
-- tag_to - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tag_to (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.tag_to%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).tag_to;

END tag_to;




-----------------------------------------------------------------
-- supplementary - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION supplementary (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.supplementary%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).supplementary;

END supplementary;


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
   )  RETURN PATCHES.supplementary%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).supplementary;

END supplementary;



-----------------------------------------------------------------
-- supplementary - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION supplementary (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.supplementary%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).supplementary;

END supplementary;




-----------------------------------------------------------------
-- patch_desc - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_desc%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).patch_desc;

END patch_desc;


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
   )  RETURN PATCHES.patch_desc%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_desc;

END patch_desc;



-----------------------------------------------------------------
-- patch_desc - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_desc%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_desc;

END patch_desc;




-----------------------------------------------------------------
-- patch_componants - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_componants%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).patch_componants;

END patch_componants;


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
   )  RETURN PATCHES.patch_componants%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_componants;

END patch_componants;



-----------------------------------------------------------------
-- patch_componants - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_componants%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_componants;

END patch_componants;




-----------------------------------------------------------------
-- patch_create_date - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_create_date%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).patch_create_date;

END patch_create_date;


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
   )  RETURN PATCHES.patch_create_date%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_create_date;

END patch_create_date;



-----------------------------------------------------------------
-- patch_create_date - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_create_date%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_create_date;

END patch_create_date;




-----------------------------------------------------------------
-- patch_created_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_created_by%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).patch_created_by;

END patch_created_by;


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
   )  RETURN PATCHES.patch_created_by%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_created_by;

END patch_created_by;



-----------------------------------------------------------------
-- patch_created_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_created_by%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_created_by;

END patch_created_by;




-----------------------------------------------------------------
-- note - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION note (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.note%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).note;

END note;


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
   )  RETURN PATCHES.note%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).note;

END note;



-----------------------------------------------------------------
-- note - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION note (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.note%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).note;

END note;




-----------------------------------------------------------------
-- log_datetime - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.log_datetime%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).log_datetime;

END log_datetime;


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
   )  RETURN PATCHES.log_datetime%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).log_datetime;

END log_datetime;



-----------------------------------------------------------------
-- log_datetime - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.log_datetime%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).log_datetime;

END log_datetime;




-----------------------------------------------------------------
-- completed_datetime - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.completed_datetime%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).completed_datetime;

END completed_datetime;


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
   )  RETURN PATCHES.completed_datetime%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).completed_datetime;

END completed_datetime;



-----------------------------------------------------------------
-- completed_datetime - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.completed_datetime%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).completed_datetime;

END completed_datetime;




-----------------------------------------------------------------
-- success_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION success_yn (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.success_yn%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).success_yn;

END success_yn;


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
   )  RETURN PATCHES.success_yn%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).success_yn;

END success_yn;



-----------------------------------------------------------------
-- success_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION success_yn (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.success_yn%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).success_yn;

END success_yn;




-----------------------------------------------------------------
-- retired_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.retired_yn%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).retired_yn;

END retired_yn;


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
   )  RETURN PATCHES.retired_yn%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).retired_yn;

END retired_yn;



-----------------------------------------------------------------
-- retired_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.retired_yn%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).retired_yn;

END retired_yn;




-----------------------------------------------------------------
-- rerunnable_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.rerunnable_yn%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).rerunnable_yn;

END rerunnable_yn;


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
   )  RETURN PATCHES.rerunnable_yn%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).rerunnable_yn;

END rerunnable_yn;



-----------------------------------------------------------------
-- rerunnable_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.rerunnable_yn%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).rerunnable_yn;

END rerunnable_yn;




-----------------------------------------------------------------
-- warning_count - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION warning_count (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.warning_count%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).warning_count;

END warning_count;


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
   )  RETURN PATCHES.warning_count%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).warning_count;

END warning_count;



-----------------------------------------------------------------
-- warning_count - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION warning_count (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.warning_count%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).warning_count;

END warning_count;




-----------------------------------------------------------------
-- error_count - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION error_count (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.error_count%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).error_count;

END error_count;


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
   )  RETURN PATCHES.error_count%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).error_count;

END error_count;



-----------------------------------------------------------------
-- error_count - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION error_count (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.error_count%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).error_count;

END error_count;




-----------------------------------------------------------------
-- username - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION username (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.username%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).username;

END username;


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
   )  RETURN PATCHES.username%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).username;

END username;



-----------------------------------------------------------------
-- username - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION username (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.username%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).username;

END username;




-----------------------------------------------------------------
-- install_log - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION install_log (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.install_log%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).install_log;

END install_log;


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
   )  RETURN PATCHES.install_log%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).install_log;

END install_log;



-----------------------------------------------------------------
-- install_log - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION install_log (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.install_log%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).install_log;

END install_log;




-----------------------------------------------------------------
-- created_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_by%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).created_by;

END created_by;


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
   )  RETURN PATCHES.created_by%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).created_by;

END created_by;



-----------------------------------------------------------------
-- created_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_by%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).created_by;

END created_by;




-----------------------------------------------------------------
-- created_on - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_on%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).created_on;

END created_on;


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
   )  RETURN PATCHES.created_on%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).created_on;

END created_on;



-----------------------------------------------------------------
-- created_on - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.created_on%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).created_on;

END created_on;




-----------------------------------------------------------------
-- last_updated_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_by%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;


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
   )  RETURN PATCHES.last_updated_by%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;



-----------------------------------------------------------------
-- last_updated_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_by%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;




-----------------------------------------------------------------
-- last_updated_on - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_on%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;


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
   )  RETURN PATCHES.last_updated_on%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;



-----------------------------------------------------------------
-- last_updated_on - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.last_updated_on%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;




-----------------------------------------------------------------
-- patch_type - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_type (
   i_patch_id IN PATCHES.PATCH_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_type%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
,i_raise_error => i_raise_error
   ).patch_type;

END patch_type;


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
   )  RETURN PATCHES.patch_type%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema => i_db_schema,
   i_branch_name => i_branch_name,
   i_tag_from => i_tag_from,
   i_tag_to => i_tag_to,
   i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_type;

END patch_type;



-----------------------------------------------------------------
-- patch_type - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_type (
   i_patch_name IN patches.patch_name%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCHES.patch_type%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_type;

END patch_type;




-----------------------------------------------------------------
-- get_current_rec
-----------------------------------------------------------------
-- get the current record by pk if given, otherwise by uk1
-----------------------------------------------------------------

FUNCTION get_current_rec(
             i_PATCHES  in PATCHES%rowtype
            ,i_raise_error IN VARCHAR2 DEFAULT 'Y'
) RETURN PATCHES%rowtype
IS

   l_PATCHES             PATCHES%rowtype := i_PATCHES;

BEGIN

  if l_PATCHES.patch_id is not null then
    --use pk to load original record
    l_PATCHES := PATCHES_tapi.PATCHES_PK(i_patch_id => i_PATCHES.patch_id ,i_raise_error  => 'Y');
  end if;


  if l_PATCHES.patch_id is null then
  --use index patches_uk2 to load original record
    l_PATCHES := PATCHES_tapi.patches_uk2(
   i_db_schema =>  i_PATCHES.DB_SCHEMA,
   i_branch_name =>  i_PATCHES.BRANCH_NAME,
   i_tag_from =>  i_PATCHES.TAG_FROM,
   i_tag_to =>  i_PATCHES.TAG_TO,
   i_supplementary =>  i_PATCHES.SUPPLEMENTARY
    );
  end if;
  if l_PATCHES.patch_id is null then
  --use index patches_uk1 to load original record
    l_PATCHES := PATCHES_tapi.patches_uk1(
   i_patch_name =>  i_PATCHES.PATCH_NAME
    );
  end if;


  if l_PATCHES.patch_id is null and i_raise_error = 'Y' then
     --!!cannot find patches
     RAISE NO_DATA_FOUND;
  end if;

  return l_PATCHES;

END;

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
) RETURN PATCHES%rowtype
IS

   l_PATCHES             PATCHES%rowtype;

BEGIN

   l_patches.patch_id        := i_patch_id;
   l_patches.patch_name        := i_patch_name;
   l_patches.db_schema        := i_db_schema;
   l_patches.branch_name        := i_branch_name;
   l_patches.tag_from        := i_tag_from;
   l_patches.tag_to        := i_tag_to;
   l_patches.supplementary        := i_supplementary;
   l_patches.patch_desc        := i_patch_desc;
   l_patches.patch_componants        := i_patch_componants;
   l_patches.patch_create_date        := i_patch_create_date;
   l_patches.patch_created_by        := i_patch_created_by;
   l_patches.note        := i_note;
   l_patches.log_datetime        := i_log_datetime;
   l_patches.completed_datetime        := i_completed_datetime;
   l_patches.success_yn        := i_success_yn;
   l_patches.retired_yn        := i_retired_yn;
   l_patches.rerunnable_yn        := i_rerunnable_yn;
   l_patches.warning_count        := i_warning_count;
   l_patches.error_count        := i_error_count;
   l_patches.username        := i_username;
   l_patches.install_log        := i_install_log;
   l_patches.created_by        := i_created_by;
   l_patches.created_on        := i_created_on;
   l_patches.last_updated_by        := i_last_updated_by;
   l_patches.last_updated_on        := i_last_updated_on;
   l_patches.patch_type        := i_patch_type;

  return l_PATCHES;

END;


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

)
IS

BEGIN

    o_patch_id := i_patches.patch_id;
    o_patch_name := i_patches.patch_name;
    o_db_schema := i_patches.db_schema;
    o_branch_name := i_patches.branch_name;
    o_tag_from := i_patches.tag_from;
    o_tag_to := i_patches.tag_to;
    o_supplementary := i_patches.supplementary;
    o_patch_desc := i_patches.patch_desc;
    o_patch_componants := i_patches.patch_componants;
    o_patch_create_date := i_patches.patch_create_date;
    o_patch_created_by := i_patches.patch_created_by;
    o_note := i_patches.note;
    o_log_datetime := i_patches.log_datetime;
    o_completed_datetime := i_patches.completed_datetime;
    o_success_yn := i_patches.success_yn;
    o_retired_yn := i_patches.retired_yn;
    o_rerunnable_yn := i_patches.rerunnable_yn;
    o_warning_count := i_patches.warning_count;
    o_error_count := i_patches.error_count;
    o_username := i_patches.username;
    o_install_log := i_patches.install_log;
    o_created_by := i_patches.created_by;
    o_created_on := i_patches.created_on;
    o_last_updated_by := i_patches.last_updated_by;
    o_last_updated_on := i_patches.last_updated_on;
    o_patch_type := i_patches.patch_type;

END;


-----------------------------------------------------------------
-- merge_old_and_new
-----------------------------------------------------------------
-- null values in NEW replaced with values from OLD
-----------------------------------------------------------------

PROCEDURE merge_old_and_new(i_old_rec  IN     patches%rowtype
                           ,io_new_rec IN OUT patches%rowtype) IS
BEGIN

  io_new_rec.patch_id              := NVL(io_new_rec.patch_id          ,i_old_rec.patch_id          );
  io_new_rec.patch_name              := NVL(io_new_rec.patch_name          ,i_old_rec.patch_name          );
  io_new_rec.db_schema              := NVL(io_new_rec.db_schema          ,i_old_rec.db_schema          );
  io_new_rec.branch_name              := NVL(io_new_rec.branch_name          ,i_old_rec.branch_name          );
  io_new_rec.tag_from              := NVL(io_new_rec.tag_from          ,i_old_rec.tag_from          );
  io_new_rec.tag_to              := NVL(io_new_rec.tag_to          ,i_old_rec.tag_to          );
  io_new_rec.supplementary              := NVL(io_new_rec.supplementary          ,i_old_rec.supplementary          );
  io_new_rec.patch_desc              := NVL(io_new_rec.patch_desc          ,i_old_rec.patch_desc          );
  io_new_rec.patch_componants              := NVL(io_new_rec.patch_componants          ,i_old_rec.patch_componants          );
  io_new_rec.patch_create_date              := NVL(io_new_rec.patch_create_date          ,i_old_rec.patch_create_date          );
  io_new_rec.patch_created_by              := NVL(io_new_rec.patch_created_by          ,i_old_rec.patch_created_by          );
  io_new_rec.note              := NVL(io_new_rec.note          ,i_old_rec.note          );
  io_new_rec.log_datetime              := NVL(io_new_rec.log_datetime          ,i_old_rec.log_datetime          );
  io_new_rec.completed_datetime              := NVL(io_new_rec.completed_datetime          ,i_old_rec.completed_datetime          );
  io_new_rec.success_yn              := NVL(io_new_rec.success_yn          ,i_old_rec.success_yn          );
  io_new_rec.retired_yn              := NVL(io_new_rec.retired_yn          ,i_old_rec.retired_yn          );
  io_new_rec.rerunnable_yn              := NVL(io_new_rec.rerunnable_yn          ,i_old_rec.rerunnable_yn          );
  io_new_rec.warning_count              := NVL(io_new_rec.warning_count          ,i_old_rec.warning_count          );
  io_new_rec.error_count              := NVL(io_new_rec.error_count          ,i_old_rec.error_count          );
  io_new_rec.username              := NVL(io_new_rec.username          ,i_old_rec.username          );
  io_new_rec.install_log              := NVL(io_new_rec.install_log          ,i_old_rec.install_log          );
  io_new_rec.created_by              := NVL(io_new_rec.created_by          ,i_old_rec.created_by          );
  io_new_rec.created_on              := NVL(io_new_rec.created_on          ,i_old_rec.created_on          );
  io_new_rec.last_updated_by              := NVL(io_new_rec.last_updated_by          ,i_old_rec.last_updated_by          );
  io_new_rec.last_updated_on              := NVL(io_new_rec.last_updated_on          ,i_old_rec.last_updated_on          );
  io_new_rec.patch_type              := NVL(io_new_rec.patch_type          ,i_old_rec.patch_type          );

END;


------------------------------------------------------------------------------
-- INSERT
------------------------------------------------------------------------------

-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using record type, returning record
-----------------------------------------------------------------


PROCEDURE ins(
    io_patches  in out patches%rowtype )
IS

BEGIN

  INSERT INTO patches VALUES io_patches
  RETURNING
   patch_id,
   patch_name,
   db_schema,
   branch_name,
   tag_from,
   tag_to,
   supplementary,
   patch_desc,
   patch_componants,
   patch_create_date,
   patch_created_by,
   note,
   log_datetime,
   completed_datetime,
   success_yn,
   retired_yn,
   rerunnable_yn,
   warning_count,
   error_count,
   username,
   install_log,
   created_by,
   created_on,
   last_updated_by,
   last_updated_on,
   patch_type
  INTO io_patches;

END;

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
)
IS

   l_patches             patches%rowtype;

BEGIN

  l_patches := create_rec(
 io_patch_id,
 io_patch_name,
 io_db_schema,
 io_branch_name,
 io_tag_from,
 io_tag_to,
 io_supplementary,
 io_patch_desc,
 io_patch_componants,
 io_patch_create_date,
 io_patch_created_by,
 io_note,
 io_log_datetime,
 io_completed_datetime,
 io_success_yn,
 io_retired_yn,
 io_rerunnable_yn,
 io_warning_count,
 io_error_count,
 io_username,
 io_install_log,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on,
 io_patch_type
 );

  ins(io_patches => l_patches);

  split_rec( l_patches,
 io_patch_id,
 io_patch_name,
 io_db_schema,
 io_branch_name,
 io_tag_from,
 io_tag_to,
 io_supplementary,
 io_patch_desc,
 io_patch_componants,
 io_patch_create_date,
 io_patch_created_by,
 io_note,
 io_log_datetime,
 io_completed_datetime,
 io_success_yn,
 io_retired_yn,
 io_rerunnable_yn,
 io_warning_count,
 io_error_count,
 io_username,
 io_install_log,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on,
 io_patch_type
);

END;

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
)
IS

   l_patches             patches%rowtype;

BEGIN

  l_patches := create_rec(
 i_patch_id,
 i_patch_name,
 i_db_schema,
 i_branch_name,
 i_tag_from,
 i_tag_to,
 i_supplementary,
 i_patch_desc,
 i_patch_componants,
 i_patch_create_date,
 i_patch_created_by,
 i_note,
 i_log_datetime,
 i_completed_datetime,
 i_success_yn,
 i_retired_yn,
 i_rerunnable_yn,
 i_warning_count,
 i_error_count,
 i_username,
 i_install_log,
 i_created_by,
 i_created_on,
 i_last_updated_by,
 i_last_updated_on,
 i_patch_type
 );

  ins(io_patches => l_patches);

END;

------------------------------------------------------------------------------
-- UPDATE
------------------------------------------------------------------------------

-----------------------------------------------------------------
-- upd
-----------------------------------------------------------------
-- update a record - using record type, returning record
-----------------------------------------------------------------


PROCEDURE upd(
    io_patches  in out patches%rowtype )
IS

BEGIN

  UPDATE patches SET ROW = io_patches
  WHERE
    patch_id = io_PATCHES.patch_id
  RETURNING
   patch_id,
   patch_name,
   db_schema,
   branch_name,
   tag_from,
   tag_to,
   supplementary,
   patch_desc,
   patch_componants,
   patch_create_date,
   patch_created_by,
   note,
   log_datetime,
   completed_datetime,
   success_yn,
   retired_yn,
   rerunnable_yn,
   warning_count,
   error_count,
   username,
   install_log,
   created_by,
   created_on,
   last_updated_by,
   last_updated_on,
   patch_type
  INTO io_patches;

END;
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
)
IS

   l_patches             patches%rowtype;

BEGIN


  l_patches := create_rec(
 io_patch_id,
 io_patch_name,
 io_db_schema,
 io_branch_name,
 io_tag_from,
 io_tag_to,
 io_supplementary,
 io_patch_desc,
 io_patch_componants,
 io_patch_create_date,
 io_patch_created_by,
 io_note,
 io_log_datetime,
 io_completed_datetime,
 io_success_yn,
 io_retired_yn,
 io_rerunnable_yn,
 io_warning_count,
 io_error_count,
 io_username,
 io_install_log,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on,
 io_patch_type
 );

  upd(io_patches => l_patches);

  split_rec( l_patches,
 io_patch_id,
 io_patch_name,
 io_db_schema,
 io_branch_name,
 io_tag_from,
 io_tag_to,
 io_supplementary,
 io_patch_desc,
 io_patch_componants,
 io_patch_create_date,
 io_patch_created_by,
 io_note,
 io_log_datetime,
 io_completed_datetime,
 io_success_yn,
 io_retired_yn,
 io_rerunnable_yn,
 io_warning_count,
 io_error_count,
 io_username,
 io_install_log,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on,
 io_patch_type
);

END;

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
)
IS

   l_new_patches  patches%rowtype;
   l_old_patches  patches%rowtype;

BEGIN

  l_new_patches := create_rec(
 i_patch_id,
 i_patch_name,
 i_db_schema,
 i_branch_name,
 i_tag_from,
 i_tag_to,
 i_supplementary,
 i_patch_desc,
 i_patch_componants,
 i_patch_create_date,
 i_patch_created_by,
 i_note,
 i_log_datetime,
 i_completed_datetime,
 i_success_yn,
 i_retired_yn,
 i_rerunnable_yn,
 i_warning_count,
 i_error_count,
 i_username,
 i_install_log,
 i_created_by,
 i_created_on,
 i_last_updated_by,
 i_last_updated_on,
 i_patch_type
 );

  l_old_patches := get_current_rec( i_patches =>  l_new_patches);

  merge_old_and_new(i_old_rec  => l_old_patches
                   ,io_new_rec => l_new_patches);

  upd(io_patches => l_new_patches);

END;


-----------------------------------------------------------------
-- upd_patches_uk1 - use uk to update itself
-----------------------------------------------------------------
PROCEDURE upd_patches_uk1 (
   i_old_patch_name IN patches.patch_name%TYPE
,
   i_new_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )
IS

   x_unknown_key EXCEPTION;
BEGIN

  UPDATE patches
    set
   patch_name = i_new_patch_name
  WHERE
   patch_name = i_old_patch_name
  ;

   IF SQL%ROWCOUNT = 0 AND
      i_raise_error = 'Y' THEN
     RAISE x_unknown_key;
   END IF;

EXCEPTION
  WHEN x_unknown_key THEN
    RAISE NO_DATA_FOUND;

END upd_patches_uk1;

------------------------------------------------------------------------------
-- DELETE
------------------------------------------------------------------------------

-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record - using record type
-----------------------------------------------------------------


PROCEDURE del(
    i_patches  in patches%rowtype )
IS

BEGIN

  DELETE patches
  WHERE
    patch_id = i_patches.patch_id
  ;

END;
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
)
IS
   l_new_patches  patches%rowtype;
   l_old_patches  patches%rowtype;

BEGIN


  l_new_patches := create_rec(
 i_patch_id,
 i_patch_name,
 i_db_schema,
 i_branch_name,
 i_tag_from,
 i_tag_to,
 i_supplementary,
 i_patch_desc,
 i_patch_componants,
 i_patch_create_date,
 i_patch_created_by,
 i_note,
 i_log_datetime,
 i_completed_datetime,
 i_success_yn,
 i_retired_yn,
 i_rerunnable_yn,
 i_warning_count,
 i_error_count,
 i_username,
 i_install_log,
 i_created_by,
 i_created_on,
 i_last_updated_by,
 i_last_updated_on,
 i_patch_type
 );

  l_old_patches := get_current_rec( i_patches =>  l_new_patches
                                       ,i_raise_error =>  i_raise_error);

  del(i_patches => l_old_patches);


END;


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
    io_patches  in out patches%rowtype )
IS

BEGIN
  BEGIN
    --Insert
    patches_tapi.ins( io_patches => io_patches );

  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN

      --update

      --Get primary key value for patches


      io_patches.patch_id := get_current_rec( i_patches =>  io_patches).patch_id;


      --Update
      patches_tapi.upd( io_patches => io_patches );

  END;
END;
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
)
IS

   l_patches             patches%rowtype;

BEGIN

  l_patches := create_rec(
 io_patch_id,
 io_patch_name,
 io_db_schema,
 io_branch_name,
 io_tag_from,
 io_tag_to,
 io_supplementary,
 io_patch_desc,
 io_patch_componants,
 io_patch_create_date,
 io_patch_created_by,
 io_note,
 io_log_datetime,
 io_completed_datetime,
 io_success_yn,
 io_retired_yn,
 io_rerunnable_yn,
 io_warning_count,
 io_error_count,
 io_username,
 io_install_log,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on,
 io_patch_type
 );

  ins_upd(io_patches => l_patches);

  split_rec( l_patches,
 io_patch_id,
 io_patch_name,
 io_db_schema,
 io_branch_name,
 io_tag_from,
 io_tag_to,
 io_supplementary,
 io_patch_desc,
 io_patch_componants,
 io_patch_create_date,
 io_patch_created_by,
 io_note,
 io_log_datetime,
 io_completed_datetime,
 io_success_yn,
 io_retired_yn,
 io_rerunnable_yn,
 io_warning_count,
 io_error_count,
 io_username,
 io_install_log,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on,
 io_patch_type
);


END;


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
)
IS

BEGIN

  ins_opt(
             i_patch_id,
             i_patch_name,
             i_db_schema,
             i_branch_name,
             i_tag_from,
             i_tag_to,
             i_supplementary,
             i_patch_desc,
             i_patch_componants,
             i_patch_create_date,
             i_patch_created_by,
             i_note,
             i_log_datetime,
             i_completed_datetime,
             i_success_yn,
             i_retired_yn,
             i_rerunnable_yn,
             i_warning_count,
             i_error_count,
             i_username,
             i_install_log,
             i_created_by,
             i_created_on,
             i_last_updated_by,
             i_last_updated_on,
             i_patch_type
 );

  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
      --update
  upd_not_null(
             i_patch_id,
             i_patch_name,
             i_db_schema,
             i_branch_name,
             i_tag_from,
             i_tag_to,
             i_supplementary,
             i_patch_desc,
             i_patch_componants,
             i_patch_create_date,
             i_patch_created_by,
             i_note,
             i_log_datetime,
             i_completed_datetime,
             i_success_yn,
             i_retired_yn,
             i_rerunnable_yn,
             i_warning_count,
             i_error_count,
             i_username,
             i_install_log,
             i_created_by,
             i_created_on,
             i_last_updated_by,
             i_last_updated_on,
             i_patch_type
 );

END;

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
RETURN BOOLEAN
IS
l_index1   PLS_INTEGER := i_collection1.FIRST;
l_index2   PLS_INTEGER := i_collection2.FIRST;
l_collections_equal     BOOLEAN     DEFAULT TRUE;

FUNCTION equal_records (
  rec1_in IN PATCHES%ROWTYPE
, rec2_in IN PATCHES%ROWTYPE
)
RETURN BOOLEAN
IS
  retval BOOLEAN;
BEGIN
  retval := rec1_in.PATCH_ID = rec2_in.PATCH_ID OR
     (rec1_in.PATCH_ID IS NULL AND rec2_in.PATCH_ID IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.PATCH_NAME = rec2_in.PATCH_NAME OR
     (rec1_in.PATCH_NAME IS NULL AND rec2_in.PATCH_NAME IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.DB_SCHEMA = rec2_in.DB_SCHEMA OR
     (rec1_in.DB_SCHEMA IS NULL AND rec2_in.DB_SCHEMA IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.BRANCH_NAME = rec2_in.BRANCH_NAME OR
     (rec1_in.BRANCH_NAME IS NULL AND rec2_in.BRANCH_NAME IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.TAG_FROM = rec2_in.TAG_FROM OR
     (rec1_in.TAG_FROM IS NULL AND rec2_in.TAG_FROM IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.TAG_TO = rec2_in.TAG_TO OR
     (rec1_in.TAG_TO IS NULL AND rec2_in.TAG_TO IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.SUPPLEMENTARY = rec2_in.SUPPLEMENTARY OR
     (rec1_in.SUPPLEMENTARY IS NULL AND rec2_in.SUPPLEMENTARY IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.PATCH_DESC = rec2_in.PATCH_DESC OR
     (rec1_in.PATCH_DESC IS NULL AND rec2_in.PATCH_DESC IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.PATCH_COMPONANTS = rec2_in.PATCH_COMPONANTS OR
     (rec1_in.PATCH_COMPONANTS IS NULL AND rec2_in.PATCH_COMPONANTS IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.PATCH_CREATE_DATE = rec2_in.PATCH_CREATE_DATE OR
     (rec1_in.PATCH_CREATE_DATE IS NULL AND rec2_in.PATCH_CREATE_DATE IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.PATCH_CREATED_BY = rec2_in.PATCH_CREATED_BY OR
     (rec1_in.PATCH_CREATED_BY IS NULL AND rec2_in.PATCH_CREATED_BY IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.NOTE = rec2_in.NOTE OR
     (rec1_in.NOTE IS NULL AND rec2_in.NOTE IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.LOG_DATETIME = rec2_in.LOG_DATETIME OR
     (rec1_in.LOG_DATETIME IS NULL AND rec2_in.LOG_DATETIME IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.COMPLETED_DATETIME = rec2_in.COMPLETED_DATETIME OR
     (rec1_in.COMPLETED_DATETIME IS NULL AND rec2_in.COMPLETED_DATETIME IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.SUCCESS_YN = rec2_in.SUCCESS_YN OR
     (rec1_in.SUCCESS_YN IS NULL AND rec2_in.SUCCESS_YN IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.RETIRED_YN = rec2_in.RETIRED_YN OR
     (rec1_in.RETIRED_YN IS NULL AND rec2_in.RETIRED_YN IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.RERUNNABLE_YN = rec2_in.RERUNNABLE_YN OR
     (rec1_in.RERUNNABLE_YN IS NULL AND rec2_in.RERUNNABLE_YN IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.WARNING_COUNT = rec2_in.WARNING_COUNT OR
     (rec1_in.WARNING_COUNT IS NULL AND rec2_in.WARNING_COUNT IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.ERROR_COUNT = rec2_in.ERROR_COUNT OR
     (rec1_in.ERROR_COUNT IS NULL AND rec2_in.ERROR_COUNT IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.USERNAME = rec2_in.USERNAME OR
     (rec1_in.USERNAME IS NULL AND rec2_in.USERNAME IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.INSTALL_LOG = rec2_in.INSTALL_LOG OR
     (rec1_in.INSTALL_LOG IS NULL AND rec2_in.INSTALL_LOG IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.CREATED_BY = rec2_in.CREATED_BY OR
     (rec1_in.CREATED_BY IS NULL AND rec2_in.CREATED_BY IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.CREATED_ON = rec2_in.CREATED_ON OR
     (rec1_in.CREATED_ON IS NULL AND rec2_in.CREATED_ON IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.LAST_UPDATED_BY = rec2_in.LAST_UPDATED_BY OR
     (rec1_in.LAST_UPDATED_BY IS NULL AND rec2_in.LAST_UPDATED_BY IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.LAST_UPDATED_ON = rec2_in.LAST_UPDATED_ON OR
     (rec1_in.LAST_UPDATED_ON IS NULL AND rec2_in.LAST_UPDATED_ON IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.PATCH_TYPE = rec2_in.PATCH_TYPE OR
     (rec1_in.PATCH_TYPE IS NULL AND rec2_in.PATCH_TYPE IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  <<unequal_records>>
  RETURN NVL (retval, FALSE);
END equal_records;
BEGIN
-- Are both collections empty?
IF l_index1 IS NULL AND l_index2 IS NULL
THEN
  l_collections_equal := NVL (i_both_null_true, FALSE);

-- Is only one empty?
ELSIF    (l_index1 IS NULL AND l_index2 IS NOT NULL)
     OR (l_index1 IS NOT NULL AND l_index2 IS NULL)
THEN
  l_collections_equal := FALSE;
ELSE
  -- Start the row by row comparisons.
  WHILE (l_index1 IS NOT NULL AND l_index2 IS NOT NULL AND l_collections_equal)
  LOOP
     -- Compare each field of both records. Are the individual values equal?
     -- Do the values match? And if for any reason, this evaluates to NULL,
     -- then treat it as FALSE.
     l_collections_equal :=
        equal_records (i_collection1 (l_index1), i_collection2 (l_index2));

     -- Do the indexes match (if that is requested)? And if for any reason,
     -- this evaluates to NULL, then treat it as FALSE.
     IF l_collections_equal AND i_match_indexes
     THEN
        l_collections_equal := NVL (l_index1 = l_index2, FALSE);
     END IF;

     -- If still equal, go to next element in each collection
     -- and make sure they both still have a value.
     IF l_collections_equal
     THEN
        l_index1 := i_collection1.NEXT (l_index1);
        l_index2 := i_collection2.NEXT (l_index2);
        l_collections_equal :=
              (l_index1 IS NOT NULL AND l_index2 IS NOT NULL
              )
           OR (l_index1 IS NULL AND l_index2 IS NULL);
     END IF;
  END LOOP;
END IF;

RETURN l_collections_equal;
END collections_equal;


END patches_tapi;
/
