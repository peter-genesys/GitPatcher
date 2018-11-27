
  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "PATCHES_TAPI" IS

-----------------------------------------------------------------
-- RECORD FUNCTIONS
-----------------------------------------------------------------

-----------------------------------------------------------------
-- patches_rid - one row from rowid
-----------------------------------------------------------------
FUNCTION patches_rid (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches%ROWTYPE  IS

   CURSOR cr_patches IS
      SELECT *
        FROM patches
       WHERE rowid = i_rowid;

   l_result patches%ROWTYPE;
   l_found   BOOLEAN;

BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
    --Unknown rowid value
     RAISE NO_DATA_FOUND;
   END IF;

   RETURN l_result;

END patches_rid;


-----------------------------------------------------------------
-- patches_pk - one row from primary key
-----------------------------------------------------------------
FUNCTION patches_pk (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches%ROWTYPE  IS

   CURSOR cr_patches IS
      SELECT *
        FROM patches
       WHERE patch_id = i_patch_id;

   l_result patches%ROWTYPE;
   l_found   BOOLEAN;

BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
      --Unknown key value
     RAISE NO_DATA_FOUND;
   END IF;

   RETURN l_result;

END patches_pk;



-----------------------------------------------------------------
-- patches_uk1 - one row from unique index
-----------------------------------------------------------------
FUNCTION patches_uk1 (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches%ROWTYPE  IS

   CURSOR cr_patches IS
      SELECT *
        FROM patches
       WHERE ((i_patch_name is null and patch_name is null) or patch_name = i_patch_name);
  
   l_result patches%ROWTYPE;
   l_found   BOOLEAN;

BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
      --Unknown key value
     RAISE NO_DATA_FOUND;
   END IF;

   RETURN l_result;

END patches_uk1;


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
   )  RETURN patches%ROWTYPE  IS

   CURSOR cr_patches IS
      SELECT *
        FROM patches
       WHERE ((i_db_schema is null and db_schema is null) or db_schema = i_db_schema    )
         AND ((i_branch_name is null and branch_name is null) or branch_name = i_branch_name  )
         AND ((i_tag_from is null and tag_from is null) or tag_from = i_tag_from     )
         AND ((i_tag_to is null and tag_to is null) or tag_to = i_tag_to       )
         AND ((i_supplementary is null and supplementary is null) or supplementary = i_supplementary);
  
   l_result patches%ROWTYPE;
   l_found   BOOLEAN;

BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
      --Unknown key value
     RAISE NO_DATA_FOUND;
   END IF;

   RETURN l_result;

END patches_uk2;




-----------------------------------------------------------------
-- COLUMN FUNCTIONS
-----------------------------------------------------------------

-----------------------------------------------------------------
-- get_rowid - rowid from primary key
-----------------------------------------------------------------
FUNCTION get_rowid (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN rowid  IS

   CURSOR cr_patches IS
      SELECT rowid
        FROM patches
       WHERE patch_id = i_patch_id;

   l_result rowid;
   l_found   BOOLEAN;

BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
      --Unknown key value
     RAISE NO_DATA_FOUND;
   END IF;

   RETURN l_result;

END get_rowid;


-----------------------------------------------------------------
-- get_rowid - rowid from unique index patches_uk1
-----------------------------------------------------------------
FUNCTION get_rowid (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN rowid  IS

   CURSOR cr_patches IS
      SELECT rowid
        FROM patches
       WHERE ((i_patch_name is null and patch_name is null) or patch_name = i_patch_name);
  
   l_result  rowid;
   l_found   BOOLEAN;

BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
      --Unknown key value
     RAISE NO_DATA_FOUND;
   END IF;

   RETURN l_result;

END get_rowid;


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
   )  RETURN rowid  IS

   CURSOR cr_patches IS
      SELECT rowid
        FROM patches
       WHERE ((i_db_schema is null and db_schema is null) or db_schema = i_db_schema    )
         AND ((i_branch_name is null and branch_name is null) or branch_name = i_branch_name  )
         AND ((i_tag_from is null and tag_from is null) or tag_from = i_tag_from     )
         AND ((i_tag_to is null and tag_to is null) or tag_to = i_tag_to       )
         AND ((i_supplementary is null and supplementary is null) or supplementary = i_supplementary);
  
   l_result  rowid;
   l_found   BOOLEAN;

BEGIN
   OPEN cr_patches;
   FETCH cr_patches INTO l_result;
   l_found := cr_patches%FOUND;
   CLOSE cr_patches;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
      --Unknown key value
     RAISE NO_DATA_FOUND;
   END IF;

   RETURN l_result;

END get_rowid;





-----------------------------------------------------------------
-- patch_id - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_id (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_id%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).patch_id;

END patch_id;

-----------------------------------------------------------------
-- patch_id - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_id (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_id%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).patch_id;

END patch_id;


-----------------------------------------------------------------
-- patch_id - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_id (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_id%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_id;

END patch_id;


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
   )  RETURN patches.patch_id%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_id;

END patch_id;


-----------------------------------------------------------------
-- patch_name - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_name (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_name%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;

-----------------------------------------------------------------
-- patch_name - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_name%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;


-----------------------------------------------------------------
-- patch_name - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_name%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;


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
   )  RETURN patches.patch_name%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;


-----------------------------------------------------------------
-- db_schema - retrieved via rowid
-----------------------------------------------------------------
FUNCTION db_schema (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.db_schema%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).db_schema;

END db_schema;

-----------------------------------------------------------------
-- db_schema - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION db_schema (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.db_schema%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).db_schema;

END db_schema;


-----------------------------------------------------------------
-- db_schema - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION db_schema (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.db_schema%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).db_schema;

END db_schema;


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
   )  RETURN patches.db_schema%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).db_schema;

END db_schema;


-----------------------------------------------------------------
-- branch_name - retrieved via rowid
-----------------------------------------------------------------
FUNCTION branch_name (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.branch_name%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).branch_name;

END branch_name;

-----------------------------------------------------------------
-- branch_name - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION branch_name (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.branch_name%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).branch_name;

END branch_name;


-----------------------------------------------------------------
-- branch_name - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION branch_name (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.branch_name%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).branch_name;

END branch_name;


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
   )  RETURN patches.branch_name%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).branch_name;

END branch_name;


-----------------------------------------------------------------
-- tag_from - retrieved via rowid
-----------------------------------------------------------------
FUNCTION tag_from (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_from%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).tag_from;

END tag_from;

-----------------------------------------------------------------
-- tag_from - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tag_from (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_from%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).tag_from;

END tag_from;


-----------------------------------------------------------------
-- tag_from - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tag_from (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_from%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).tag_from;

END tag_from;


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
   )  RETURN patches.tag_from%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).tag_from;

END tag_from;


-----------------------------------------------------------------
-- tag_to - retrieved via rowid
-----------------------------------------------------------------
FUNCTION tag_to (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_to%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).tag_to;

END tag_to;

-----------------------------------------------------------------
-- tag_to - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tag_to (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_to%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).tag_to;

END tag_to;


-----------------------------------------------------------------
-- tag_to - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tag_to (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tag_to%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).tag_to;

END tag_to;


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
   )  RETURN patches.tag_to%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).tag_to;

END tag_to;


-----------------------------------------------------------------
-- supplementary - retrieved via rowid
-----------------------------------------------------------------
FUNCTION supplementary (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.supplementary%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).supplementary;

END supplementary;

-----------------------------------------------------------------
-- supplementary - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION supplementary (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.supplementary%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).supplementary;

END supplementary;


-----------------------------------------------------------------
-- supplementary - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION supplementary (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.supplementary%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).supplementary;

END supplementary;


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
   )  RETURN patches.supplementary%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).supplementary;

END supplementary;


-----------------------------------------------------------------
-- patch_desc - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_desc%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).patch_desc;

END patch_desc;

-----------------------------------------------------------------
-- patch_desc - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_desc%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).patch_desc;

END patch_desc;


-----------------------------------------------------------------
-- patch_desc - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_desc (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_desc%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_desc;

END patch_desc;


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
   )  RETURN patches.patch_desc%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_desc;

END patch_desc;


-----------------------------------------------------------------
-- patch_componants - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_componants%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).patch_componants;

END patch_componants;

-----------------------------------------------------------------
-- patch_componants - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_componants%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).patch_componants;

END patch_componants;


-----------------------------------------------------------------
-- patch_componants - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_componants (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_componants%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_componants;

END patch_componants;


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
   )  RETURN patches.patch_componants%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_componants;

END patch_componants;


-----------------------------------------------------------------
-- patch_create_date - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_create_date%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).patch_create_date;

END patch_create_date;

-----------------------------------------------------------------
-- patch_create_date - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_create_date%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).patch_create_date;

END patch_create_date;


-----------------------------------------------------------------
-- patch_create_date - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_create_date (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_create_date%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_create_date;

END patch_create_date;


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
   )  RETURN patches.patch_create_date%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_create_date;

END patch_create_date;


-----------------------------------------------------------------
-- patch_created_by - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_created_by%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).patch_created_by;

END patch_created_by;

-----------------------------------------------------------------
-- patch_created_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_created_by%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).patch_created_by;

END patch_created_by;


-----------------------------------------------------------------
-- patch_created_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_created_by (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_created_by%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_created_by;

END patch_created_by;


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
   )  RETURN patches.patch_created_by%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_created_by;

END patch_created_by;


-----------------------------------------------------------------
-- note - retrieved via rowid
-----------------------------------------------------------------
FUNCTION note (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.note%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).note;

END note;

-----------------------------------------------------------------
-- note - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION note (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.note%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).note;

END note;


-----------------------------------------------------------------
-- note - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION note (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.note%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).note;

END note;


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
   )  RETURN patches.note%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).note;

END note;


-----------------------------------------------------------------
-- log_datetime - retrieved via rowid
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.log_datetime%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).log_datetime;

END log_datetime;

-----------------------------------------------------------------
-- log_datetime - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.log_datetime%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).log_datetime;

END log_datetime;


-----------------------------------------------------------------
-- log_datetime - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION log_datetime (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.log_datetime%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).log_datetime;

END log_datetime;


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
   )  RETURN patches.log_datetime%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).log_datetime;

END log_datetime;


-----------------------------------------------------------------
-- completed_datetime - retrieved via rowid
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.completed_datetime%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).completed_datetime;

END completed_datetime;

-----------------------------------------------------------------
-- completed_datetime - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.completed_datetime%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).completed_datetime;

END completed_datetime;


-----------------------------------------------------------------
-- completed_datetime - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION completed_datetime (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.completed_datetime%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).completed_datetime;

END completed_datetime;


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
   )  RETURN patches.completed_datetime%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).completed_datetime;

END completed_datetime;


-----------------------------------------------------------------
-- success_yn - retrieved via rowid
-----------------------------------------------------------------
FUNCTION success_yn (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.success_yn%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).success_yn;

END success_yn;

-----------------------------------------------------------------
-- success_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION success_yn (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.success_yn%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).success_yn;

END success_yn;


-----------------------------------------------------------------
-- success_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION success_yn (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.success_yn%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).success_yn;

END success_yn;


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
   )  RETURN patches.success_yn%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).success_yn;

END success_yn;


-----------------------------------------------------------------
-- retired_yn - retrieved via rowid
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.retired_yn%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).retired_yn;

END retired_yn;

-----------------------------------------------------------------
-- retired_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.retired_yn%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).retired_yn;

END retired_yn;


-----------------------------------------------------------------
-- retired_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION retired_yn (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.retired_yn%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).retired_yn;

END retired_yn;


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
   )  RETURN patches.retired_yn%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).retired_yn;

END retired_yn;


-----------------------------------------------------------------
-- rerunnable_yn - retrieved via rowid
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.rerunnable_yn%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).rerunnable_yn;

END rerunnable_yn;

-----------------------------------------------------------------
-- rerunnable_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.rerunnable_yn%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).rerunnable_yn;

END rerunnable_yn;


-----------------------------------------------------------------
-- rerunnable_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION rerunnable_yn (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.rerunnable_yn%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).rerunnable_yn;

END rerunnable_yn;


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
   )  RETURN patches.rerunnable_yn%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).rerunnable_yn;

END rerunnable_yn;


-----------------------------------------------------------------
-- warning_count - retrieved via rowid
-----------------------------------------------------------------
FUNCTION warning_count (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.warning_count%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).warning_count;

END warning_count;

-----------------------------------------------------------------
-- warning_count - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION warning_count (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.warning_count%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).warning_count;

END warning_count;


-----------------------------------------------------------------
-- warning_count - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION warning_count (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.warning_count%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).warning_count;

END warning_count;


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
   )  RETURN patches.warning_count%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).warning_count;

END warning_count;


-----------------------------------------------------------------
-- error_count - retrieved via rowid
-----------------------------------------------------------------
FUNCTION error_count (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.error_count%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).error_count;

END error_count;

-----------------------------------------------------------------
-- error_count - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION error_count (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.error_count%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).error_count;

END error_count;


-----------------------------------------------------------------
-- error_count - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION error_count (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.error_count%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).error_count;

END error_count;


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
   )  RETURN patches.error_count%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).error_count;

END error_count;


-----------------------------------------------------------------
-- username - retrieved via rowid
-----------------------------------------------------------------
FUNCTION username (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.username%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).username;

END username;

-----------------------------------------------------------------
-- username - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION username (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.username%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).username;

END username;


-----------------------------------------------------------------
-- username - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION username (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.username%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).username;

END username;


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
   )  RETURN patches.username%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).username;

END username;


-----------------------------------------------------------------
-- install_log - retrieved via rowid
-----------------------------------------------------------------
FUNCTION install_log (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.install_log%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).install_log;

END install_log;

-----------------------------------------------------------------
-- install_log - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION install_log (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.install_log%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).install_log;

END install_log;


-----------------------------------------------------------------
-- install_log - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION install_log (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.install_log%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).install_log;

END install_log;


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
   )  RETURN patches.install_log%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).install_log;

END install_log;


-----------------------------------------------------------------
-- created_by - retrieved via rowid
-----------------------------------------------------------------
FUNCTION created_by (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_by%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).created_by;

END created_by;

-----------------------------------------------------------------
-- created_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_by%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).created_by;

END created_by;


-----------------------------------------------------------------
-- created_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_by%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).created_by;

END created_by;


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
   )  RETURN patches.created_by%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).created_by;

END created_by;


-----------------------------------------------------------------
-- created_on - retrieved via rowid
-----------------------------------------------------------------
FUNCTION created_on (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_on%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).created_on;

END created_on;

-----------------------------------------------------------------
-- created_on - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_on%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).created_on;

END created_on;


-----------------------------------------------------------------
-- created_on - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.created_on%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).created_on;

END created_on;


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
   )  RETURN patches.created_on%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).created_on;

END created_on;


-----------------------------------------------------------------
-- last_updated_by - retrieved via rowid
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_by%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;

-----------------------------------------------------------------
-- last_updated_by - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_by%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;


-----------------------------------------------------------------
-- last_updated_by - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_by%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;


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
   )  RETURN patches.last_updated_by%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;


-----------------------------------------------------------------
-- last_updated_on - retrieved via rowid
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_on%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;

-----------------------------------------------------------------
-- last_updated_on - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_on%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;


-----------------------------------------------------------------
-- last_updated_on - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.last_updated_on%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;


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
   )  RETURN patches.last_updated_on%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;


-----------------------------------------------------------------
-- patch_type - retrieved via rowid
-----------------------------------------------------------------
FUNCTION patch_type (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_type%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).patch_type;

END patch_type;

-----------------------------------------------------------------
-- patch_type - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION patch_type (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_type%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).patch_type;

END patch_type;


-----------------------------------------------------------------
-- patch_type - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION patch_type (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.patch_type%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).patch_type;

END patch_type;


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
   )  RETURN patches.patch_type%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).patch_type;

END patch_type;


-----------------------------------------------------------------
-- tracking_yn - retrieved via rowid
-----------------------------------------------------------------
FUNCTION tracking_yn (
   i_rowid IN rowid
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tracking_yn%TYPE
IS

BEGIN
  RETURN patches_rid (
   i_rowid => i_rowid
  ,i_raise_error => i_raise_error
   ).tracking_yn;

END tracking_yn;

-----------------------------------------------------------------
-- tracking_yn - retrieved via primary key patches_pk
-----------------------------------------------------------------
FUNCTION tracking_yn (
   i_patch_id IN patches.patch_id%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tracking_yn%TYPE
IS

BEGIN
  RETURN patches_pk (
   i_patch_id => i_patch_id
  ,i_raise_error => i_raise_error
   ).tracking_yn;

END tracking_yn;


-----------------------------------------------------------------
-- tracking_yn - retrieved via unique index patches_uk1
-----------------------------------------------------------------
FUNCTION tracking_yn (
   i_patch_name IN patches.patch_name%TYPE
  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN patches.tracking_yn%TYPE
IS

BEGIN
  RETURN patches_uk1(
   i_patch_name => i_patch_name
,i_raise_error => i_raise_error
   ).tracking_yn;

END tracking_yn;


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
   )  RETURN patches.tracking_yn%TYPE
IS

BEGIN
  RETURN patches_uk2(
   i_db_schema     => i_db_schema    
  ,i_branch_name   => i_branch_name  
  ,i_tag_from      => i_tag_from     
  ,i_tag_to        => i_tag_to       
  ,i_supplementary => i_supplementary
,i_raise_error => i_raise_error
   ).tracking_yn;

END tracking_yn;




-----------------------------------------------------------------
-- get_rowid
-----------------------------------------------------------------
-- get the current record rowid by pk if given, otherwise by uk1
-----------------------------------------------------------------

FUNCTION get_rowid(
    i_patches  in patches%rowtype
   ,i_raise_error        IN VARCHAR2 DEFAULT 'N' ) return rowid
IS
  l_rowid rowid;
  l_pk_given boolean := false;
BEGIN


  l_pk_given := i_patches.patch_id is not null;
  if l_pk_given then
    --Get Rowid from PK
    l_rowid := get_rowid(
     i_patch_id => i_patches.patch_id
  ,i_raise_error => i_raise_error);
  end if;



  if not l_pk_given and l_rowid is null then
    --Get Rowid from patches_uk1
    l_rowid := get_rowid (
     i_patch_name => i_patches.patch_name
   ,i_raise_error => i_raise_error
    );
  end if;


  if not l_pk_given and l_rowid is null then
    --Get Rowid from patches_uk2
    l_rowid := get_rowid (
     i_db_schema     => i_patches.db_schema    
    ,i_branch_name   => i_patches.branch_name  
    ,i_tag_from      => i_patches.tag_from     
    ,i_tag_to        => i_patches.tag_to       
    ,i_supplementary => i_patches.supplementary
   ,i_raise_error => i_raise_error
    );
  end if;


   IF l_rowid is null AND
      i_raise_error = 'Y' THEN
    --Cannot determine record from PK or UK
     RAISE NO_DATA_FOUND;
   END IF;

   RETURN l_rowid;

END;

-----------------------------------------------------------------
-- get_current_rec
-----------------------------------------------------------------
-- get the current record by pk if given, otherwise by uk1
-----------------------------------------------------------------

FUNCTION get_current_rec(
             i_patches IN patches%rowtype
            ,i_raise_error IN VARCHAR2 DEFAULT 'Y'
) RETURN patches%rowtype IS

   l_patches   patches%rowtype := i_patches;
   l_rowid rowid;

BEGIN

  l_rowid := get_rowid(
      i_patches  => i_patches
     ,i_raise_error        => i_raise_error );

  l_patches := patches_rid (
     i_rowid               => l_rowid
    ,i_raise_error  => i_raise_error );

  return l_patches;

END get_current_rec;


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
) RETURN patches%ROWTYPE IS

   l_patches             patches%rowtype;

BEGIN

   l_patches.patch_id           := i_patch_id          ;
   l_patches.patch_name         := i_patch_name        ;
   l_patches.db_schema          := i_db_schema         ;
   l_patches.branch_name        := i_branch_name       ;
   l_patches.tag_from           := i_tag_from          ;
   l_patches.tag_to             := i_tag_to            ;
   l_patches.supplementary      := i_supplementary     ;
   l_patches.patch_desc         := i_patch_desc        ;
   l_patches.patch_componants   := i_patch_componants  ;
   l_patches.patch_create_date  := i_patch_create_date ;
   l_patches.patch_created_by   := i_patch_created_by  ;
   l_patches.note               := i_note              ;
   l_patches.log_datetime       := i_log_datetime      ;
   l_patches.completed_datetime := i_completed_datetime;
   l_patches.success_yn         := i_success_yn        ;
   l_patches.retired_yn         := i_retired_yn        ;
   l_patches.rerunnable_yn      := i_rerunnable_yn     ;
   l_patches.warning_count      := i_warning_count     ;
   l_patches.error_count        := i_error_count       ;
   l_patches.username           := i_username          ;
   l_patches.install_log        := i_install_log       ;
   l_patches.created_by         := i_created_by        ;
   l_patches.created_on         := i_created_on        ;
   l_patches.last_updated_by    := i_last_updated_by   ;
   l_patches.last_updated_on    := i_last_updated_on   ;
   l_patches.patch_type         := i_patch_type        ;
   l_patches.tracking_yn        := i_tracking_yn       ;

  return l_patches;

END create_rec;


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
) IS

BEGIN

   o_patch_id           := i_patches.patch_id;
   o_patch_name         := i_patches.patch_name;
   o_db_schema          := i_patches.db_schema;
   o_branch_name        := i_patches.branch_name;
   o_tag_from           := i_patches.tag_from;
   o_tag_to             := i_patches.tag_to;
   o_supplementary      := i_patches.supplementary;
   o_patch_desc         := i_patches.patch_desc;
   o_patch_componants   := i_patches.patch_componants;
   o_patch_create_date  := i_patches.patch_create_date;
   o_patch_created_by   := i_patches.patch_created_by;
   o_note               := i_patches.note;
   o_log_datetime       := i_patches.log_datetime;
   o_completed_datetime := i_patches.completed_datetime;
   o_success_yn         := i_patches.success_yn;
   o_retired_yn         := i_patches.retired_yn;
   o_rerunnable_yn      := i_patches.rerunnable_yn;
   o_warning_count      := i_patches.warning_count;
   o_error_count        := i_patches.error_count;
   o_username           := i_patches.username;
   o_install_log        := i_patches.install_log;
   o_created_by         := i_patches.created_by;
   o_created_on         := i_patches.created_on;
   o_last_updated_by    := i_patches.last_updated_by;
   o_last_updated_on    := i_patches.last_updated_on;
   o_patch_type         := i_patches.patch_type;
   o_tracking_yn        := i_patches.tracking_yn;

END;



-----------------------------------------------------------------
-- merge_old_and_new
-----------------------------------------------------------------
-- null values in NEW replaced with values from OLD
-----------------------------------------------------------------

PROCEDURE merge_old_and_new(i_old_rec  IN     patches%rowtype
                           ,io_new_rec IN OUT patches%rowtype) IS
BEGIN

  io_new_rec.patch_id           := NVL(io_new_rec.patch_id          ,i_old_rec.patch_id          );
  io_new_rec.patch_name         := NVL(io_new_rec.patch_name        ,i_old_rec.patch_name        );
  io_new_rec.db_schema          := NVL(io_new_rec.db_schema         ,i_old_rec.db_schema         );
  io_new_rec.branch_name        := NVL(io_new_rec.branch_name       ,i_old_rec.branch_name       );
  io_new_rec.tag_from           := NVL(io_new_rec.tag_from          ,i_old_rec.tag_from          );
  io_new_rec.tag_to             := NVL(io_new_rec.tag_to            ,i_old_rec.tag_to            );
  io_new_rec.supplementary      := NVL(io_new_rec.supplementary     ,i_old_rec.supplementary     );
  io_new_rec.patch_desc         := NVL(io_new_rec.patch_desc        ,i_old_rec.patch_desc        );
  --INVALID for CLOB,BLOB io_new_rec.patch_componants   := NVL(io_new_rec.patch_componants  ,i_old_rec.patch_componants  );
  io_new_rec.patch_create_date  := NVL(io_new_rec.patch_create_date ,i_old_rec.patch_create_date );
  io_new_rec.patch_created_by   := NVL(io_new_rec.patch_created_by  ,i_old_rec.patch_created_by  );
  io_new_rec.note               := NVL(io_new_rec.note              ,i_old_rec.note              );
  io_new_rec.log_datetime       := NVL(io_new_rec.log_datetime      ,i_old_rec.log_datetime      );
  io_new_rec.completed_datetime := NVL(io_new_rec.completed_datetime,i_old_rec.completed_datetime);
  io_new_rec.success_yn         := NVL(io_new_rec.success_yn        ,i_old_rec.success_yn        );
  io_new_rec.retired_yn         := NVL(io_new_rec.retired_yn        ,i_old_rec.retired_yn        );
  io_new_rec.rerunnable_yn      := NVL(io_new_rec.rerunnable_yn     ,i_old_rec.rerunnable_yn     );
  io_new_rec.warning_count      := NVL(io_new_rec.warning_count     ,i_old_rec.warning_count     );
  io_new_rec.error_count        := NVL(io_new_rec.error_count       ,i_old_rec.error_count       );
  io_new_rec.username           := NVL(io_new_rec.username          ,i_old_rec.username          );
  --INVALID for CLOB,BLOB io_new_rec.install_log        := NVL(io_new_rec.install_log       ,i_old_rec.install_log       );
  io_new_rec.created_by         := NVL(io_new_rec.created_by        ,i_old_rec.created_by        );
  io_new_rec.created_on         := NVL(io_new_rec.created_on        ,i_old_rec.created_on        );
  io_new_rec.last_updated_by    := NVL(io_new_rec.last_updated_by   ,i_old_rec.last_updated_by   );
  io_new_rec.last_updated_on    := NVL(io_new_rec.last_updated_on   ,i_old_rec.last_updated_on   );
  io_new_rec.patch_type         := NVL(io_new_rec.patch_type        ,i_old_rec.patch_type        );
  io_new_rec.tracking_yn        := NVL(io_new_rec.tracking_yn       ,i_old_rec.tracking_yn       );

END;


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
   ,io_rowid             in out rowid )
IS

BEGIN

  --Insert the record, returning the rowid.
  INSERT INTO patches VALUES io_patches RETURNING rowid into io_rowid;

  --Retrieve the full record, as columns may have been changed by triggers.
  io_patches := patches_rid(i_rowid => io_rowid);

END ins;

-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using record type, returning record
--   uses returning clause.
--   Suitable for tables with or without long columns
--   Does not return rowid.
-----------------------------------------------------------------

PROCEDURE ins(
    io_patches  in out patches%rowtype )
IS
  l_rowid rowid;
BEGIN

  ins(
    io_patches  => io_patches
   ,io_rowid             => l_rowid);

END ins;


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
) IS

   l_patches             patches%rowtype;

BEGIN

  l_patches := create_rec(
     io_patch_id          
    ,io_patch_name        
    ,io_db_schema         
    ,io_branch_name       
    ,io_tag_from          
    ,io_tag_to            
    ,io_supplementary     
    ,io_patch_desc        
    ,io_patch_componants  
    ,io_patch_create_date 
    ,io_patch_created_by  
    ,io_note              
    ,io_log_datetime      
    ,io_completed_datetime
    ,io_success_yn        
    ,io_retired_yn        
    ,io_rerunnable_yn     
    ,io_warning_count     
    ,io_error_count       
    ,io_username          
    ,io_install_log       
    ,io_created_by        
    ,io_created_on        
    ,io_last_updated_by   
    ,io_last_updated_on   
    ,io_patch_type        
    ,io_tracking_yn       
 );

  ins(io_patches => l_patches);

  split_rec( l_patches,
     io_patch_id          
    ,io_patch_name        
    ,io_db_schema         
    ,io_branch_name       
    ,io_tag_from          
    ,io_tag_to            
    ,io_supplementary     
    ,io_patch_desc        
    ,io_patch_componants  
    ,io_patch_create_date 
    ,io_patch_created_by  
    ,io_note              
    ,io_log_datetime      
    ,io_completed_datetime
    ,io_success_yn        
    ,io_retired_yn        
    ,io_rerunnable_yn     
    ,io_warning_count     
    ,io_error_count       
    ,io_username          
    ,io_install_log       
    ,io_created_by        
    ,io_created_on        
    ,io_last_updated_by   
    ,io_last_updated_on   
    ,io_patch_type        
    ,io_tracking_yn       
);

END ins;


-----------------------------------------------------------------
-- ins_opt
-----------------------------------------------------------------
-- insert a record - using components, all optional
-- Does not return rowid.
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
)
IS

   l_patches             patches%rowtype;

BEGIN

  l_patches := create_rec(
     i_patch_id          
    ,i_patch_name        
    ,i_db_schema         
    ,i_branch_name       
    ,i_tag_from          
    ,i_tag_to            
    ,i_supplementary     
    ,i_patch_desc        
    ,i_patch_componants  
    ,i_patch_create_date 
    ,i_patch_created_by  
    ,i_note              
    ,i_log_datetime      
    ,i_completed_datetime
    ,i_success_yn        
    ,i_retired_yn        
    ,i_rerunnable_yn     
    ,i_warning_count     
    ,i_error_count       
    ,i_username          
    ,i_install_log       
    ,i_created_by        
    ,i_created_on        
    ,i_last_updated_by   
    ,i_last_updated_on   
    ,i_patch_type        
    ,i_tracking_yn       
 );

  ins(io_patches => l_patches);

END ins_opt;

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
   ,io_rowid             in out rowid )
IS

BEGIN

  --Update record, retrieving rowid again, incase it has changed.
  UPDATE patches SET ROW = io_patches where rowid = io_rowid RETURNING rowid into io_rowid;

  --Retrieve the full record, as columns may have been changed by triggers.
  io_patches := patches_rid(i_rowid => io_rowid);

END upd;


-----------------------------------------------------------------
-- upd
-----------------------------------------------------------------
-- update a record - using record type, primary key cols, returning record
--   uses returning clause.
--   Suitable for tables with or without long columns
--   Does not return rowid.
-----------------------------------------------------------------

PROCEDURE upd(
    io_patches  in out patches%rowtype )
IS
  l_rowid rowid;
BEGIN

  --Get Rowid
  l_rowid := get_rowid(
      i_patches  => io_patches
     ,i_raise_error        => 'Y' );

  upd(
    io_patches  => io_patches
   ,io_rowid              => l_rowid);

END upd;


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
)
IS

   l_patches            patches%rowtype;

BEGIN

  l_patches := create_rec(
     io_patch_id          
    ,io_patch_name        
    ,io_db_schema         
    ,io_branch_name       
    ,io_tag_from          
    ,io_tag_to            
    ,io_supplementary     
    ,io_patch_desc        
    ,io_patch_componants  
    ,io_patch_create_date 
    ,io_patch_created_by  
    ,io_note              
    ,io_log_datetime      
    ,io_completed_datetime
    ,io_success_yn        
    ,io_retired_yn        
    ,io_rerunnable_yn     
    ,io_warning_count     
    ,io_error_count       
    ,io_username          
    ,io_install_log       
    ,io_created_by        
    ,io_created_on        
    ,io_last_updated_by   
    ,io_last_updated_on   
    ,io_patch_type        
    ,io_tracking_yn       
 );

  upd(io_patches => l_patches);

  split_rec( l_patches,
     io_patch_id          
    ,io_patch_name        
    ,io_db_schema         
    ,io_branch_name       
    ,io_tag_from          
    ,io_tag_to            
    ,io_supplementary     
    ,io_patch_desc        
    ,io_patch_componants  
    ,io_patch_create_date 
    ,io_patch_created_by  
    ,io_note              
    ,io_log_datetime      
    ,io_completed_datetime
    ,io_success_yn        
    ,io_retired_yn        
    ,io_rerunnable_yn     
    ,io_warning_count     
    ,io_error_count       
    ,io_username          
    ,io_install_log       
    ,io_created_by        
    ,io_created_on        
    ,io_last_updated_by   
    ,io_last_updated_on   
    ,io_patch_type        
    ,io_tracking_yn       
);


END upd;


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
)
IS

   l_new_patches  patches%rowtype;
   l_old_patches  patches%rowtype;

BEGIN

  l_new_patches := create_rec(
     i_patch_id          
    ,i_patch_name        
    ,i_db_schema         
    ,i_branch_name       
    ,i_tag_from          
    ,i_tag_to            
    ,i_supplementary     
    ,i_patch_desc        
    ,i_patch_componants  
    ,i_patch_create_date 
    ,i_patch_created_by  
    ,i_note              
    ,i_log_datetime      
    ,i_completed_datetime
    ,i_success_yn        
    ,i_retired_yn        
    ,i_rerunnable_yn     
    ,i_warning_count     
    ,i_error_count       
    ,i_username          
    ,i_install_log       
    ,i_created_by        
    ,i_created_on        
    ,i_last_updated_by   
    ,i_last_updated_on   
    ,i_patch_type        
    ,i_tracking_yn       
 );

  l_old_patches := get_current_rec( i_patches =>  l_new_patches
                                       ,i_raise_error =>  'Y');

  merge_old_and_new(i_old_rec  => l_old_patches
                   ,io_new_rec => l_new_patches);

  upd(io_patches => l_new_patches);

END upd_not_null;

-----------------------------------------------------------------
-- upd_patches_uk1 - use uk to update itself
-----------------------------------------------------------------

PROCEDURE upd_patches_uk1 (
     i_old_patch_name IN patches.patch_name%TYPE
      ,i_new_patch_name IN patches.patch_name%TYPE

  ,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )
IS

BEGIN

  UPDATE patches
     SET patch_name = i_new_patch_name
       WHERE patch_name = i_old_patch_name
  ;

   IF SQL%ROWCOUNT = 0 AND
      i_raise_error = 'Y' THEN
    --Unknown key
     RAISE NO_DATA_FOUND;
   END IF;

END upd_patches_uk1;

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
   )
IS

BEGIN

  UPDATE patches
     SET db_schema     = i_new_db_schema    
       , branch_name   = i_new_branch_name  
       , tag_from      = i_new_tag_from     
       , tag_to        = i_new_tag_to       
       , supplementary = i_new_supplementary
       WHERE db_schema     = i_old_db_schema    
     AND branch_name   = i_old_branch_name  
     AND tag_from      = i_old_tag_from     
     AND tag_to        = i_old_tag_to       
     AND supplementary = i_old_supplementary
  ;

   IF SQL%ROWCOUNT = 0 AND
      i_raise_error = 'Y' THEN
    --Unknown key
     RAISE NO_DATA_FOUND;
   END IF;

END upd_patches_uk2;




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
   ,i_raise_error  IN VARCHAR2 DEFAULT 'N' )
IS

BEGIN

    DELETE patches
    WHERE rowid = i_rowid;

   IF SQL%ROWCOUNT = 0    AND
      i_raise_error = 'Y' THEN
    --Record missing
     RAISE NO_DATA_FOUND;
   END IF;

END del;


-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record - using record type
-----------------------------------------------------------------


PROCEDURE del(
    i_patches  in patches%rowtype
   ,i_raise_error        IN VARCHAR2 DEFAULT 'N' )
IS
  l_rowid rowid;
BEGIN

  --Get Rowid
  l_rowid := get_rowid(
      i_patches  => i_patches
     ,i_raise_error        => i_raise_error );

  del(i_rowid       => l_rowid
     ,i_raise_error => i_raise_error);

END del;



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
)
IS
   l_patches  patches%rowtype;

BEGIN


  l_patches := create_rec(
     i_patch_id          
    ,i_patch_name        
    ,i_db_schema         
    ,i_branch_name       
    ,i_tag_from          
    ,i_tag_to            
    ,i_supplementary     
    ,i_patch_desc        
    ,i_patch_componants  
    ,i_patch_create_date 
    ,i_patch_created_by  
    ,i_note              
    ,i_log_datetime      
    ,i_completed_datetime
    ,i_success_yn        
    ,i_retired_yn        
    ,i_rerunnable_yn     
    ,i_warning_count     
    ,i_error_count       
    ,i_username          
    ,i_install_log       
    ,i_created_by        
    ,i_created_on        
    ,i_last_updated_by   
    ,i_last_updated_on   
    ,i_patch_type        
    ,i_tracking_yn       
 );

  del(i_patches => l_patches
     ,i_raise_error       => i_raise_error);


END del;


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

      --Update
      patches_tapi.upd( io_patches => io_patches );

  END;
END ins_upd;



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
) IS

   l_patches             patches%rowtype;

BEGIN

  l_patches := create_rec(
     io_patch_id          
    ,io_patch_name        
    ,io_db_schema         
    ,io_branch_name       
    ,io_tag_from          
    ,io_tag_to            
    ,io_supplementary     
    ,io_patch_desc        
    ,io_patch_componants  
    ,io_patch_create_date 
    ,io_patch_created_by  
    ,io_note              
    ,io_log_datetime      
    ,io_completed_datetime
    ,io_success_yn        
    ,io_retired_yn        
    ,io_rerunnable_yn     
    ,io_warning_count     
    ,io_error_count       
    ,io_username          
    ,io_install_log       
    ,io_created_by        
    ,io_created_on        
    ,io_last_updated_by   
    ,io_last_updated_on   
    ,io_patch_type        
    ,io_tracking_yn       
 );

  ins_upd(io_patches => l_patches);

  split_rec( l_patches,
     io_patch_id          
    ,io_patch_name        
    ,io_db_schema         
    ,io_branch_name       
    ,io_tag_from          
    ,io_tag_to            
    ,io_supplementary     
    ,io_patch_desc        
    ,io_patch_componants  
    ,io_patch_create_date 
    ,io_patch_created_by  
    ,io_note              
    ,io_log_datetime      
    ,io_completed_datetime
    ,io_success_yn        
    ,io_retired_yn        
    ,io_rerunnable_yn     
    ,io_warning_count     
    ,io_error_count       
    ,io_username          
    ,io_install_log       
    ,io_created_by        
    ,io_created_on        
    ,io_last_updated_by   
    ,io_last_updated_on   
    ,io_patch_type        
    ,io_tracking_yn       
);


END ins_upd;



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
) IS

BEGIN

  ins_opt(
     i_patch_id          
    ,i_patch_name        
    ,i_db_schema         
    ,i_branch_name       
    ,i_tag_from          
    ,i_tag_to            
    ,i_supplementary     
    ,i_patch_desc        
    ,i_patch_componants  
    ,i_patch_create_date 
    ,i_patch_created_by  
    ,i_note              
    ,i_log_datetime      
    ,i_completed_datetime
    ,i_success_yn        
    ,i_retired_yn        
    ,i_rerunnable_yn     
    ,i_warning_count     
    ,i_error_count       
    ,i_username          
    ,i_install_log       
    ,i_created_by        
    ,i_created_on        
    ,i_last_updated_by   
    ,i_last_updated_on   
    ,i_patch_type        
    ,i_tracking_yn       
 );

  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
      --update
  upd_not_null(
     i_patch_id          
    ,i_patch_name        
    ,i_db_schema         
    ,i_branch_name       
    ,i_tag_from          
    ,i_tag_to            
    ,i_supplementary     
    ,i_patch_desc        
    ,i_patch_componants  
    ,i_patch_create_date 
    ,i_patch_created_by  
    ,i_note              
    ,i_log_datetime      
    ,i_completed_datetime
    ,i_success_yn        
    ,i_retired_yn        
    ,i_rerunnable_yn     
    ,i_warning_count     
    ,i_error_count       
    ,i_username          
    ,i_install_log       
    ,i_created_by        
    ,i_created_on        
    ,i_last_updated_by   
    ,i_last_updated_on   
    ,i_patch_type        
    ,i_tracking_yn       
 );

END ins_upd_not_null;


------------------------------------------------------------------------------
-- DATA UNLOADING
------------------------------------------------------------------------------


------------------------------------------------------------------------------
-- unload_data
------------------------------------------------------------------------------
-- unload data into a script ins_upd statements
-- in PK order if possible..
-- return this script as a clob?
-- or could be a pipelined table function, that is spooled from SQL
------------------------------------------------------------------------------




procedure unload_data is

  l_template clob :=
q'!
patches_tapi.ins_upd_not_null(
     i_patch_id           => '{PATCH_ID}'
    ,i_patch_name         => '{PATCH_NAME}'
    ,i_db_schema          => '{DB_SCHEMA}'
    ,i_branch_name        => '{BRANCH_NAME}'
    ,i_tag_from           => '{TAG_FROM}'
    ,i_tag_to             => '{TAG_TO}'
    ,i_supplementary      => '{SUPPLEMENTARY}'
    ,i_patch_desc         => '{PATCH_DESC}'
    ,i_patch_componants   => '{PATCH_COMPONANTS}'
    ,i_patch_create_date  => '{PATCH_CREATE_DATE}'
    ,i_patch_created_by   => '{PATCH_CREATED_BY}'
    ,i_note               => '{NOTE}'
    ,i_log_datetime       => '{LOG_DATETIME}'
    ,i_completed_datetime => '{COMPLETED_DATETIME}'
    ,i_success_yn         => '{SUCCESS_YN}'
    ,i_retired_yn         => '{RETIRED_YN}'
    ,i_rerunnable_yn      => '{RERUNNABLE_YN}'
    ,i_warning_count      => '{WARNING_COUNT}'
    ,i_error_count        => '{ERROR_COUNT}'
    ,i_username           => '{USERNAME}'
    ,i_install_log        => '{INSTALL_LOG}'
    ,i_created_by         => '{CREATED_BY}'
    ,i_created_on         => '{CREATED_ON}'
    ,i_last_updated_by    => '{LAST_UPDATED_BY}'
    ,i_last_updated_on    => '{LAST_UPDATED_ON}'
    ,i_patch_type         => '{PATCH_TYPE}'
    ,i_tracking_yn        => '{TRACKING_YN}'
);
!';


begin
  dbms_output.put_line('PROMPT Reloading data into patches');
  dbms_output.put_line('BEGIN');
  for l_patches in (
     select *
     from patches
     order by
              patch_id           ) loop

    declare
      l_ins_upd CLOB := l_template;

    begin
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_ID}' , l_patches.patch_id);
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_NAME}' , l_patches.patch_name);
      l_ins_upd   := REPLACE(l_ins_upd, '{DB_SCHEMA}' , l_patches.db_schema);
      l_ins_upd   := REPLACE(l_ins_upd, '{BRANCH_NAME}' , l_patches.branch_name);
      l_ins_upd   := REPLACE(l_ins_upd, '{TAG_FROM}' , l_patches.tag_from);
      l_ins_upd   := REPLACE(l_ins_upd, '{TAG_TO}' , l_patches.tag_to);
      l_ins_upd   := REPLACE(l_ins_upd, '{SUPPLEMENTARY}' , l_patches.supplementary);
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_DESC}' , l_patches.patch_desc);
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_COMPONANTS}' , l_patches.patch_componants);
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_CREATE_DATE}' , l_patches.patch_create_date);
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_CREATED_BY}' , l_patches.patch_created_by);
      l_ins_upd   := REPLACE(l_ins_upd, '{NOTE}' , l_patches.note);
      l_ins_upd   := REPLACE(l_ins_upd, '{LOG_DATETIME}' , l_patches.log_datetime);
      l_ins_upd   := REPLACE(l_ins_upd, '{COMPLETED_DATETIME}' , l_patches.completed_datetime);
      l_ins_upd   := REPLACE(l_ins_upd, '{SUCCESS_YN}' , l_patches.success_yn);
      l_ins_upd   := REPLACE(l_ins_upd, '{RETIRED_YN}' , l_patches.retired_yn);
      l_ins_upd   := REPLACE(l_ins_upd, '{RERUNNABLE_YN}' , l_patches.rerunnable_yn);
      l_ins_upd   := REPLACE(l_ins_upd, '{WARNING_COUNT}' , l_patches.warning_count);
      l_ins_upd   := REPLACE(l_ins_upd, '{ERROR_COUNT}' , l_patches.error_count);
      l_ins_upd   := REPLACE(l_ins_upd, '{USERNAME}' , l_patches.username);
      l_ins_upd   := REPLACE(l_ins_upd, '{INSTALL_LOG}' , l_patches.install_log);
      l_ins_upd   := REPLACE(l_ins_upd, '{CREATED_BY}' , l_patches.created_by);
      l_ins_upd   := REPLACE(l_ins_upd, '{CREATED_ON}' , l_patches.created_on);
      l_ins_upd   := REPLACE(l_ins_upd, '{LAST_UPDATED_BY}' , l_patches.last_updated_by);
      l_ins_upd   := REPLACE(l_ins_upd, '{LAST_UPDATED_ON}' , l_patches.last_updated_on);
      l_ins_upd   := REPLACE(l_ins_upd, '{PATCH_TYPE}' , l_patches.patch_type);
      l_ins_upd   := REPLACE(l_ins_upd, '{TRACKING_YN}' , l_patches.tracking_yn);

      dbms_output.put_line(l_ins_upd);
    end;

  end loop;
  dbms_output.put_line('END;');
  dbms_output.put_line('/');
  dbms_output.put_line('PROMPT Dataload complete!');

end unload_data;




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
RETURN BOOLEAN
IS
l_index1   PLS_INTEGER := i_collection1.FIRST;
l_index2   PLS_INTEGER := i_collection2.FIRST;
l_collections_equal     BOOLEAN     DEFAULT TRUE;

  FUNCTION equal_records ( rec1_in IN patches%ROWTYPE
                         , rec2_in IN patches%ROWTYPE ) RETURN BOOLEAN
  IS
    retval BOOLEAN;
  BEGIN
    retval := rec1_in.patch_id = rec2_in.patch_id OR
   (rec1_in.patch_id IS NULL AND rec2_in.patch_id IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.patch_name = rec2_in.patch_name OR
   (rec1_in.patch_name IS NULL AND rec2_in.patch_name IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.db_schema = rec2_in.db_schema OR
   (rec1_in.db_schema IS NULL AND rec2_in.db_schema IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.branch_name = rec2_in.branch_name OR
   (rec1_in.branch_name IS NULL AND rec2_in.branch_name IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.tag_from = rec2_in.tag_from OR
   (rec1_in.tag_from IS NULL AND rec2_in.tag_from IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.tag_to = rec2_in.tag_to OR
   (rec1_in.tag_to IS NULL AND rec2_in.tag_to IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.supplementary = rec2_in.supplementary OR
   (rec1_in.supplementary IS NULL AND rec2_in.supplementary IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.patch_desc = rec2_in.patch_desc OR
   (rec1_in.patch_desc IS NULL AND rec2_in.patch_desc IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    --INVALID for CLOB,BLOB retval := rec1_in.patch_componants = rec2_in.patch_componants OR
--INVALID for CLOB,BLOB    (rec1_in.patch_componants IS NULL AND rec2_in.patch_componants IS NULL);
--INVALID for CLOB,BLOB IF NOT NVL (retval, FALSE) THEN
--INVALID for CLOB,BLOB   RETURN FALSE;
--INVALID for CLOB,BLOB END IF;
    retval := rec1_in.patch_create_date = rec2_in.patch_create_date OR
   (rec1_in.patch_create_date IS NULL AND rec2_in.patch_create_date IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.patch_created_by = rec2_in.patch_created_by OR
   (rec1_in.patch_created_by IS NULL AND rec2_in.patch_created_by IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.note = rec2_in.note OR
   (rec1_in.note IS NULL AND rec2_in.note IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.log_datetime = rec2_in.log_datetime OR
   (rec1_in.log_datetime IS NULL AND rec2_in.log_datetime IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.completed_datetime = rec2_in.completed_datetime OR
   (rec1_in.completed_datetime IS NULL AND rec2_in.completed_datetime IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.success_yn = rec2_in.success_yn OR
   (rec1_in.success_yn IS NULL AND rec2_in.success_yn IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.retired_yn = rec2_in.retired_yn OR
   (rec1_in.retired_yn IS NULL AND rec2_in.retired_yn IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.rerunnable_yn = rec2_in.rerunnable_yn OR
   (rec1_in.rerunnable_yn IS NULL AND rec2_in.rerunnable_yn IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.warning_count = rec2_in.warning_count OR
   (rec1_in.warning_count IS NULL AND rec2_in.warning_count IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.error_count = rec2_in.error_count OR
   (rec1_in.error_count IS NULL AND rec2_in.error_count IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.username = rec2_in.username OR
   (rec1_in.username IS NULL AND rec2_in.username IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    --INVALID for CLOB,BLOB retval := rec1_in.install_log = rec2_in.install_log OR
--INVALID for CLOB,BLOB    (rec1_in.install_log IS NULL AND rec2_in.install_log IS NULL);
--INVALID for CLOB,BLOB IF NOT NVL (retval, FALSE) THEN
--INVALID for CLOB,BLOB   RETURN FALSE;
--INVALID for CLOB,BLOB END IF;
    retval := rec1_in.created_by = rec2_in.created_by OR
   (rec1_in.created_by IS NULL AND rec2_in.created_by IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.created_on = rec2_in.created_on OR
   (rec1_in.created_on IS NULL AND rec2_in.created_on IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.last_updated_by = rec2_in.last_updated_by OR
   (rec1_in.last_updated_by IS NULL AND rec2_in.last_updated_by IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.last_updated_on = rec2_in.last_updated_on OR
   (rec1_in.last_updated_on IS NULL AND rec2_in.last_updated_on IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.patch_type = rec2_in.patch_type OR
   (rec1_in.patch_type IS NULL AND rec2_in.patch_type IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;
    retval := rec1_in.tracking_yn = rec2_in.tracking_yn OR
   (rec1_in.tracking_yn IS NULL AND rec2_in.tracking_yn IS NULL);
IF NOT NVL (retval, FALSE) THEN
  RETURN FALSE;
END IF;

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
        equal_records (i_collection1 (l_index1)
                     , i_collection2 (l_index2));

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


end patches_tapi;
/
