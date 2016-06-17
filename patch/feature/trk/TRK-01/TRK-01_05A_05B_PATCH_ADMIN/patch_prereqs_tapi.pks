CREATE OR REPLACE PACKAGE patch_prereqs_tapi IS

----------------------------------------------------------------
-- Used by COLLECTION FUNCTIONS
----------------------------------------------------------------

TYPE patch_prereqs_aat IS TABLE OF PATCH_PREREQS%ROWTYPE
   INDEX BY BINARY_INTEGER;


-----------------------------------------------------------------
-- RECORD FUNCTIONS
-----------------------------------------------------------------

-----------------------------------------------------------------
-- patch_prereqs_pk - one row from primary key
-----------------------------------------------------------------
FUNCTION patch_prereqs_pk (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS%ROWTYPE RESULT_CACHE;

-----------------------------------------------------------------
-- patch_prereqs_uk1 - one row from unique index
-----------------------------------------------------------------
FUNCTION patch_prereqs_uk1 (
   i_patch_name IN PATCH_PREREQS.PATCH_NAME%TYPE,
   i_prereq_patch IN PATCH_PREREQS.PREREQ_PATCH%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS%ROWTYPE RESULT_CACHE;



-----------------------------------------------------------------
-- COLUMN FUNCTIONS
-----------------------------------------------------------------


-----------------------------------------------------------------
-- patch_name - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.patch_name%TYPE;





-----------------------------------------------------------------
-- patch_name - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.patch_name%TYPE;





-----------------------------------------------------------------
-- prereq_patch - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION prereq_patch (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.prereq_patch%TYPE;





-----------------------------------------------------------------
-- prereq_patch - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION prereq_patch (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.prereq_patch%TYPE;





-----------------------------------------------------------------
-- created_by - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.created_by%TYPE;





-----------------------------------------------------------------
-- created_by - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.created_by%TYPE;





-----------------------------------------------------------------
-- created_on - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.created_on%TYPE;





-----------------------------------------------------------------
-- created_on - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.created_on%TYPE;





-----------------------------------------------------------------
-- last_updated_by - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.last_updated_by%TYPE;





-----------------------------------------------------------------
-- last_updated_by - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.last_updated_by%TYPE;





-----------------------------------------------------------------
-- last_updated_on - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.last_updated_on%TYPE;





-----------------------------------------------------------------
-- last_updated_on - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.last_updated_on%TYPE;





-----------------------------------------------------------------
-- get_current_rec
-----------------------------------------------------------------
-- get the current record by pk if given, otherwise by uk1
-----------------------------------------------------------------

FUNCTION get_current_rec(
             i_PATCH_PREREQS  in PATCH_PREREQS%rowtype
            ,i_raise_error IN VARCHAR2 DEFAULT 'Y'
) RETURN PATCH_PREREQS%rowtype;
-----------------------------------------------------------------
-- create_rec
-----------------------------------------------------------------
-- create a record from its component fields
-----------------------------------------------------------------

FUNCTION create_rec(
             i_patch_prereq_id  in patch_prereqs.patch_prereq_id%type DEFAULT NULL,
             i_patch_name  in patch_prereqs.patch_name%type DEFAULT NULL,
             i_prereq_patch  in patch_prereqs.prereq_patch%type DEFAULT NULL,
             i_created_by  in patch_prereqs.created_by%type DEFAULT NULL,
             i_created_on  in patch_prereqs.created_on%type DEFAULT NULL,
             i_last_updated_by  in patch_prereqs.last_updated_by%type DEFAULT NULL,
             i_last_updated_on  in patch_prereqs.last_updated_on%type DEFAULT NULL
) RETURN PATCH_PREREQS%rowtype;


-----------------------------------------------------------------
-- split_rec
-----------------------------------------------------------------
-- split a record into its component fields
-----------------------------------------------------------------

PROCEDURE split_rec( i_patch_prereqs in patch_prereqs%rowtype,
             o_patch_prereq_id  out patch_prereqs.patch_prereq_id%type,
             o_patch_name  out patch_prereqs.patch_name%type,
             o_prereq_patch  out patch_prereqs.prereq_patch%type,
             o_created_by  out patch_prereqs.created_by%type,
             o_created_on  out patch_prereqs.created_on%type,
             o_last_updated_by  out patch_prereqs.last_updated_by%type,
             o_last_updated_on  out patch_prereqs.last_updated_on%type

);


-----------------------------------------------------------------
-- merge_old_and_new
-----------------------------------------------------------------
-- null values in NEW replaced with values from OLD
-----------------------------------------------------------------

PROCEDURE merge_old_and_new(i_old_rec  IN     patch_prereqs%rowtype
                           ,io_new_rec IN OUT patch_prereqs%rowtype);

------------------------------------------------------------------------------
-- INSERT
------------------------------------------------------------------------------
-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using record type, returning record
-----------------------------------------------------------------
PROCEDURE ins(
    io_patch_prereqs  in out patch_prereqs%rowtype ) ;
-----------------------------------------------------------------
-- ins
-----------------------------------------------------------------
-- insert a record - using components, returning components
-----------------------------------------------------------------

PROCEDURE ins(
             io_patch_prereq_id  in out patch_prereqs.patch_prereq_id%type,
             io_patch_name  in out patch_prereqs.patch_name%type,
             io_prereq_patch  in out patch_prereqs.prereq_patch%type,
             io_created_by  in out patch_prereqs.created_by%type,
             io_created_on  in out patch_prereqs.created_on%type,
             io_last_updated_by  in out patch_prereqs.last_updated_by%type,
             io_last_updated_on  in out patch_prereqs.last_updated_on%type
) ;

-----------------------------------------------------------------
-- ins_opt
-----------------------------------------------------------------
-- insert a record - using components, all optional
-----------------------------------------------------------------
PROCEDURE ins_opt(
             i_patch_prereq_id  in patch_prereqs.patch_prereq_id%type DEFAULT NULL,
             i_patch_name  in patch_prereqs.patch_name%type DEFAULT NULL,
             i_prereq_patch  in patch_prereqs.prereq_patch%type DEFAULT NULL,
             i_created_by  in patch_prereqs.created_by%type DEFAULT NULL,
             i_created_on  in patch_prereqs.created_on%type DEFAULT NULL,
             i_last_updated_by  in patch_prereqs.last_updated_by%type DEFAULT NULL,
             i_last_updated_on  in patch_prereqs.last_updated_on%type DEFAULT NULL
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
    io_patch_prereqs  in out patch_prereqs%rowtype )  ;
-----------------------------------------------------------------
-- upd
-----------------------------------------------------------------
-- update a record - using components, returning components
-----------------------------------------------------------------
PROCEDURE upd(
             io_patch_prereq_id  in out patch_prereqs.patch_prereq_id%type,
             io_patch_name  in out patch_prereqs.patch_name%type,
             io_prereq_patch  in out patch_prereqs.prereq_patch%type,
             io_created_by  in out patch_prereqs.created_by%type,
             io_created_on  in out patch_prereqs.created_on%type,
             io_last_updated_by  in out patch_prereqs.last_updated_by%type,
             io_last_updated_on  in out patch_prereqs.last_updated_on%type
) ;
-------------------------------------------------------------------------
-- upd_not_null
-------------------------------------------------------------------------
-- update a record
--   using components, all optional
--   by pk if given, otherwise by uk1, null values ignored.
-----------------------------------------------------------------
PROCEDURE upd_not_null(
             i_patch_prereq_id  in patch_prereqs.patch_prereq_id%type DEFAULT NULL,
             i_patch_name  in patch_prereqs.patch_name%type DEFAULT NULL,
             i_prereq_patch  in patch_prereqs.prereq_patch%type DEFAULT NULL,
             i_created_by  in patch_prereqs.created_by%type DEFAULT NULL,
             i_created_on  in patch_prereqs.created_on%type DEFAULT NULL,
             i_last_updated_by  in patch_prereqs.last_updated_by%type DEFAULT NULL,
             i_last_updated_on  in patch_prereqs.last_updated_on%type DEFAULT NULL
);


-----------------------------------------------------------------
-- upd_patch_prereqs_uk1 - use uk to update itself
-----------------------------------------------------------------
PROCEDURE upd_patch_prereqs_uk1 (
   i_old_patch_name IN patch_prereqs.patch_name%TYPE,
   i_old_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,
   i_new_patch_name IN patch_prereqs.patch_name%TYPE,
   i_new_prereq_patch IN patch_prereqs.prereq_patch%TYPE
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
    i_patch_prereqs  in patch_prereqs%rowtype ) ;
-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record
--   using components, all optional
--   by pk if given, otherwise by uk1
-----------------------------------------------------------------

PROCEDURE del(
             i_patch_prereq_id  in patch_prereqs.patch_prereq_id%type default null,
             i_patch_name  in patch_prereqs.patch_name%type default null,
             i_prereq_patch  in patch_prereqs.prereq_patch%type default null,
             i_created_by  in patch_prereqs.created_by%type default null,
             i_created_on  in patch_prereqs.created_on%type default null,
             i_last_updated_by  in patch_prereqs.last_updated_by%type default null,
             i_last_updated_on  in patch_prereqs.last_updated_on%type default null
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
    io_patch_prereqs  in out patch_prereqs%rowtype ) ;
-----------------------------------------------------------------
-- ins_upd
-----------------------------------------------------------------
-- insert or update a record using components, returning components
-- insert a record - if possible
-- update a record - by pk if given, otherwise by uk1
-----------------------------------------------------------------

PROCEDURE ins_upd(
             io_patch_prereq_id  in out patch_prereqs.patch_prereq_id%type,
             io_patch_name  in out patch_prereqs.patch_name%type,
             io_prereq_patch  in out patch_prereqs.prereq_patch%type,
             io_created_by  in out patch_prereqs.created_by%type,
             io_created_on  in out patch_prereqs.created_on%type,
             io_last_updated_by  in out patch_prereqs.last_updated_by%type,
             io_last_updated_on  in out patch_prereqs.last_updated_on%type
) ;

-----------------------------------------------------------------
-- ins_upd
-----------------------------------------------------------------
-- insert or update a record using components, all optional
-- insert a record - if possible
-- update a record - by pk if given, otherwise by uk1, null values ignored.
-----------------------------------------------------------------

PROCEDURE ins_upd_not_null(
             i_patch_prereq_id  in patch_prereqs.patch_prereq_id%type default null,
             i_patch_name  in patch_prereqs.patch_name%type default null,
             i_prereq_patch  in patch_prereqs.prereq_patch%type default null,
             i_created_by  in patch_prereqs.created_by%type default null,
             i_created_on  in patch_prereqs.created_on%type default null,
             i_last_updated_by  in patch_prereqs.last_updated_by%type default null,
             i_last_updated_on  in patch_prereqs.last_updated_on%type default null
);

------------------------------------------------------------------------------
-- COLLECTION FUNCTIONS
------------------------------------------------------------------------------
-----------------------------------------------------------------
-- collections_equal
-----------------------------------------------------------------
-- Return true if the the contents of the two collections are the same.
-- In this variant, the collection is based on the rowtype of the
-- the table PATCH_PREREQS,
-- i_collection1    - first collection for comparison
-- i_collection2    - second collection for comparison
-- i_match_indexes  - if TRUE, then the row numbers in the two
--                         collections must also match.
-- i_both_null_true - if TRUE, then if values in corresponding rows
--                         of both collections are NULL, treat this as equality.
-----------------------------------------------------------------

FUNCTION collections_equal (
      i_collection1     IN   patch_prereqs_aat
    , i_collection2     IN   patch_prereqs_aat
    , i_match_indexes   IN   BOOLEAN DEFAULT TRUE
    , i_both_null_true  IN   BOOLEAN DEFAULT TRUE
   )
      RETURN BOOLEAN;


END patch_prereqs_tapi;
/
