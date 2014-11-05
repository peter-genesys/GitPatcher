CREATE OR REPLACE PACKAGE BODY patch_prereqs_tapi IS

-----------------------------------------------------------------
-- RECORD FUNCTIONS
-----------------------------------------------------------------

-----------------------------------------------------------------
-- patch_prereqs_pk - one row from primary key
-----------------------------------------------------------------
FUNCTION patch_prereqs_pk (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS%ROWTYPE RESULT_CACHE RELIES_ON (patch_prereqs)
IS
   CURSOR cr_patch_prereqs
   IS
      SELECT *
        FROM patch_prereqs
       WHERE
          PATCH_PREREQ_ID = i_patch_prereq_id
      ;
   l_result PATCH_PREREQS%ROWTYPE;
   l_found   BOOLEAN;
   x_unknown_key EXCEPTION;
BEGIN
   OPEN cr_patch_prereqs;
   FETCH cr_patch_prereqs INTO l_result;
   l_found := cr_patch_prereqs%FOUND;
   CLOSE cr_patch_prereqs;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
     RAISE x_unknown_key;
   END IF;

   RETURN l_result;

EXCEPTION
  WHEN x_unknown_key THEN
    RAISE NO_DATA_FOUND;

END patch_prereqs_pk;


-----------------------------------------------------------------
-- patch_prereqs_uk1 - one row from unique index
-----------------------------------------------------------------
FUNCTION patch_prereqs_uk1 (
   i_patch_name IN PATCH_PREREQS.PATCH_NAME%TYPE,
   i_prereq_patch IN PATCH_PREREQS.PREREQ_PATCH%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   ) RETURN PATCH_PREREQS%ROWTYPE RESULT_CACHE RELIES_ON (patch_prereqs)
IS
   CURSOR cr_patch_prereqs IS
      SELECT *
        FROM PATCH_PREREQS
       WHERE
            (i_patch_name is null or PATCH_NAME = i_patch_name) AND
            (i_prereq_patch is null or PREREQ_PATCH = i_prereq_patch)
      ;
   l_result PATCH_PREREQS%ROWTYPE;
   l_found   BOOLEAN;
   x_unknown_key EXCEPTION;
BEGIN
   OPEN cr_patch_prereqs;
   FETCH cr_patch_prereqs INTO l_result;
   l_found := cr_patch_prereqs%FOUND;
   CLOSE cr_patch_prereqs;

   IF NOT l_found AND
      i_raise_error = 'Y' THEN
     RAISE x_unknown_key;
   END IF;

   RETURN l_result;

EXCEPTION
  WHEN x_unknown_key THEN
    RAISE NO_DATA_FOUND;

END patch_prereqs_uk1;



-----------------------------------------------------------------
-- COLUMN FUNCTIONS
-----------------------------------------------------------------


-----------------------------------------------------------------
-- patch_name - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.patch_name%TYPE
IS

BEGIN
  RETURN patch_prereqs_pk (
   i_patch_prereq_id => i_patch_prereq_id
,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;


-----------------------------------------------------------------
-- patch_name - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION patch_name (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.patch_name%TYPE
IS

BEGIN
  RETURN patch_prereqs_uk1(
   i_patch_name => i_patch_name,
   i_prereq_patch => i_prereq_patch
,i_raise_error => i_raise_error
   ).patch_name;

END patch_name;




-----------------------------------------------------------------
-- prereq_patch - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION prereq_patch (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.prereq_patch%TYPE
IS

BEGIN
  RETURN patch_prereqs_pk (
   i_patch_prereq_id => i_patch_prereq_id
,i_raise_error => i_raise_error
   ).prereq_patch;

END prereq_patch;


-----------------------------------------------------------------
-- prereq_patch - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION prereq_patch (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.prereq_patch%TYPE
IS

BEGIN
  RETURN patch_prereqs_uk1(
   i_patch_name => i_patch_name,
   i_prereq_patch => i_prereq_patch
,i_raise_error => i_raise_error
   ).prereq_patch;

END prereq_patch;




-----------------------------------------------------------------
-- created_by - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.created_by%TYPE
IS

BEGIN
  RETURN patch_prereqs_pk (
   i_patch_prereq_id => i_patch_prereq_id
,i_raise_error => i_raise_error
   ).created_by;

END created_by;


-----------------------------------------------------------------
-- created_by - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION created_by (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.created_by%TYPE
IS

BEGIN
  RETURN patch_prereqs_uk1(
   i_patch_name => i_patch_name,
   i_prereq_patch => i_prereq_patch
,i_raise_error => i_raise_error
   ).created_by;

END created_by;




-----------------------------------------------------------------
-- created_on - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.created_on%TYPE
IS

BEGIN
  RETURN patch_prereqs_pk (
   i_patch_prereq_id => i_patch_prereq_id
,i_raise_error => i_raise_error
   ).created_on;

END created_on;


-----------------------------------------------------------------
-- created_on - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION created_on (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.created_on%TYPE
IS

BEGIN
  RETURN patch_prereqs_uk1(
   i_patch_name => i_patch_name,
   i_prereq_patch => i_prereq_patch
,i_raise_error => i_raise_error
   ).created_on;

END created_on;




-----------------------------------------------------------------
-- last_updated_by - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.last_updated_by%TYPE
IS

BEGIN
  RETURN patch_prereqs_pk (
   i_patch_prereq_id => i_patch_prereq_id
,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;


-----------------------------------------------------------------
-- last_updated_by - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION last_updated_by (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.last_updated_by%TYPE
IS

BEGIN
  RETURN patch_prereqs_uk1(
   i_patch_name => i_patch_name,
   i_prereq_patch => i_prereq_patch
,i_raise_error => i_raise_error
   ).last_updated_by;

END last_updated_by;




-----------------------------------------------------------------
-- last_updated_on - retrieved via primary key patch_prereqs_pk
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_prereq_id IN PATCH_PREREQS.PATCH_PREREQ_ID%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.last_updated_on%TYPE
IS

BEGIN
  RETURN patch_prereqs_pk (
   i_patch_prereq_id => i_patch_prereq_id
,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;


-----------------------------------------------------------------
-- last_updated_on - retrieved via unique index patch_prereqs_uk1
-----------------------------------------------------------------
FUNCTION last_updated_on (
   i_patch_name IN patch_prereqs.patch_name%TYPE,
   i_prereq_patch IN patch_prereqs.prereq_patch%TYPE
,i_raise_error IN VARCHAR2 DEFAULT 'N'
   )  RETURN PATCH_PREREQS.last_updated_on%TYPE
IS

BEGIN
  RETURN patch_prereqs_uk1(
   i_patch_name => i_patch_name,
   i_prereq_patch => i_prereq_patch
,i_raise_error => i_raise_error
   ).last_updated_on;

END last_updated_on;




-----------------------------------------------------------------
-- get_current_rec
-----------------------------------------------------------------
-- get the current record by pk if given, otherwise by uk1
-----------------------------------------------------------------

FUNCTION get_current_rec(
             i_PATCH_PREREQS  in PATCH_PREREQS%rowtype
            ,i_raise_error IN VARCHAR2 DEFAULT 'Y'
) RETURN PATCH_PREREQS%rowtype
IS

   l_PATCH_PREREQS             PATCH_PREREQS%rowtype := i_PATCH_PREREQS;

BEGIN

  if l_PATCH_PREREQS.patch_prereq_id is not null then
    --use pk to load original record
    l_PATCH_PREREQS := PATCH_PREREQS_tapi.PATCH_PREREQS_PK(i_patch_prereq_id => i_PATCH_PREREQS.patch_prereq_id ,i_raise_error  => 'Y');
  end if;


  if l_PATCH_PREREQS.patch_prereq_id is null then
  --use index patch_prereqs_uk1 to load original record
    l_PATCH_PREREQS := PATCH_PREREQS_tapi.patch_prereqs_uk1(
   i_patch_name =>  i_PATCH_PREREQS.PATCH_NAME,
   i_prereq_patch =>  i_PATCH_PREREQS.PREREQ_PATCH
    );
  end if;


  if l_PATCH_PREREQS.patch_prereq_id is null and i_raise_error = 'Y' then
     --!!cannot find patch_prereqs
     RAISE NO_DATA_FOUND;
  end if;

  return l_PATCH_PREREQS;

END;

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
) RETURN PATCH_PREREQS%rowtype
IS

   l_PATCH_PREREQS             PATCH_PREREQS%rowtype;

BEGIN

   l_patch_prereqs.patch_prereq_id        := i_patch_prereq_id;
   l_patch_prereqs.patch_name        := i_patch_name;
   l_patch_prereqs.prereq_patch        := i_prereq_patch;
   l_patch_prereqs.created_by        := i_created_by;
   l_patch_prereqs.created_on        := i_created_on;
   l_patch_prereqs.last_updated_by        := i_last_updated_by;
   l_patch_prereqs.last_updated_on        := i_last_updated_on;

  return l_PATCH_PREREQS;

END;


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

)
IS

BEGIN

    o_patch_prereq_id := i_patch_prereqs.patch_prereq_id;
    o_patch_name := i_patch_prereqs.patch_name;
    o_prereq_patch := i_patch_prereqs.prereq_patch;
    o_created_by := i_patch_prereqs.created_by;
    o_created_on := i_patch_prereqs.created_on;
    o_last_updated_by := i_patch_prereqs.last_updated_by;
    o_last_updated_on := i_patch_prereqs.last_updated_on;

END;


-----------------------------------------------------------------
-- merge_old_and_new
-----------------------------------------------------------------
-- null values in NEW replaced with values from OLD
-----------------------------------------------------------------

PROCEDURE merge_old_and_new(i_old_rec  IN     patch_prereqs%rowtype
                           ,io_new_rec IN OUT patch_prereqs%rowtype) IS
BEGIN

  io_new_rec.patch_prereq_id              := NVL(io_new_rec.patch_prereq_id          ,i_old_rec.patch_prereq_id          );
  io_new_rec.patch_name              := NVL(io_new_rec.patch_name          ,i_old_rec.patch_name          );
  io_new_rec.prereq_patch              := NVL(io_new_rec.prereq_patch          ,i_old_rec.prereq_patch          );
  io_new_rec.created_by              := NVL(io_new_rec.created_by          ,i_old_rec.created_by          );
  io_new_rec.created_on              := NVL(io_new_rec.created_on          ,i_old_rec.created_on          );
  io_new_rec.last_updated_by              := NVL(io_new_rec.last_updated_by          ,i_old_rec.last_updated_by          );
  io_new_rec.last_updated_on              := NVL(io_new_rec.last_updated_on          ,i_old_rec.last_updated_on          );

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
    io_patch_prereqs  in out patch_prereqs%rowtype )
IS

BEGIN

  INSERT INTO patch_prereqs VALUES io_patch_prereqs
  RETURNING
   patch_prereq_id,
   patch_name,
   prereq_patch,
   created_by,
   created_on,
   last_updated_by,
   last_updated_on
  INTO io_patch_prereqs;

END;

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
)
IS

   l_patch_prereqs             patch_prereqs%rowtype;

BEGIN

  l_patch_prereqs := create_rec(
 io_patch_prereq_id,
 io_patch_name,
 io_prereq_patch,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on
 );

  ins(io_patch_prereqs => l_patch_prereqs);

  split_rec( l_patch_prereqs,
 io_patch_prereq_id,
 io_patch_name,
 io_prereq_patch,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on
);

END;

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
)
IS

   l_patch_prereqs             patch_prereqs%rowtype;

BEGIN

  l_patch_prereqs := create_rec(
 i_patch_prereq_id,
 i_patch_name,
 i_prereq_patch,
 i_created_by,
 i_created_on,
 i_last_updated_by,
 i_last_updated_on
 );

  ins(io_patch_prereqs => l_patch_prereqs);

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
    io_patch_prereqs  in out patch_prereqs%rowtype )
IS

BEGIN

  UPDATE patch_prereqs SET ROW = io_patch_prereqs
  WHERE
    patch_prereq_id = io_PATCH_PREREQS.patch_prereq_id
  RETURNING
   patch_prereq_id,
   patch_name,
   prereq_patch,
   created_by,
   created_on,
   last_updated_by,
   last_updated_on
  INTO io_patch_prereqs;

END;
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
)
IS

   l_patch_prereqs             patch_prereqs%rowtype;

BEGIN


  l_patch_prereqs := create_rec(
 io_patch_prereq_id,
 io_patch_name,
 io_prereq_patch,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on
 );

  upd(io_patch_prereqs => l_patch_prereqs);

  split_rec( l_patch_prereqs,
 io_patch_prereq_id,
 io_patch_name,
 io_prereq_patch,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on
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
             i_patch_prereq_id  in patch_prereqs.patch_prereq_id%type DEFAULT NULL,
             i_patch_name  in patch_prereqs.patch_name%type DEFAULT NULL,
             i_prereq_patch  in patch_prereqs.prereq_patch%type DEFAULT NULL,
             i_created_by  in patch_prereqs.created_by%type DEFAULT NULL,
             i_created_on  in patch_prereqs.created_on%type DEFAULT NULL,
             i_last_updated_by  in patch_prereqs.last_updated_by%type DEFAULT NULL,
             i_last_updated_on  in patch_prereqs.last_updated_on%type DEFAULT NULL
)
IS

   l_new_patch_prereqs  patch_prereqs%rowtype;
   l_old_patch_prereqs  patch_prereqs%rowtype;

BEGIN

  l_new_patch_prereqs := create_rec(
 i_patch_prereq_id,
 i_patch_name,
 i_prereq_patch,
 i_created_by,
 i_created_on,
 i_last_updated_by,
 i_last_updated_on
 );

  l_old_patch_prereqs := get_current_rec( i_patch_prereqs =>  l_new_patch_prereqs);

  merge_old_and_new(i_old_rec  => l_old_patch_prereqs
                   ,io_new_rec => l_new_patch_prereqs);

  upd(io_patch_prereqs => l_new_patch_prereqs);

END;


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
   )
IS

   x_unknown_key EXCEPTION;
BEGIN

  UPDATE patch_prereqs
    set
   patch_name = i_new_patch_name,
   prereq_patch = i_new_prereq_patch
  WHERE
   patch_name = i_old_patch_name AND
   prereq_patch = i_old_prereq_patch
  ;

   IF SQL%ROWCOUNT = 0 AND
      i_raise_error = 'Y' THEN
     RAISE x_unknown_key;
   END IF;

EXCEPTION
  WHEN x_unknown_key THEN
    RAISE NO_DATA_FOUND;

END upd_patch_prereqs_uk1;

------------------------------------------------------------------------------
-- DELETE
------------------------------------------------------------------------------

-----------------------------------------------------------------
-- del
-----------------------------------------------------------------
-- delete a record - using record type
-----------------------------------------------------------------


PROCEDURE del(
    i_patch_prereqs  in patch_prereqs%rowtype )
IS

BEGIN

  DELETE patch_prereqs
  WHERE
    patch_prereq_id = i_patch_prereqs.patch_prereq_id
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
             i_patch_prereq_id  in patch_prereqs.patch_prereq_id%type default null,
             i_patch_name  in patch_prereqs.patch_name%type default null,
             i_prereq_patch  in patch_prereqs.prereq_patch%type default null,
             i_created_by  in patch_prereqs.created_by%type default null,
             i_created_on  in patch_prereqs.created_on%type default null,
             i_last_updated_by  in patch_prereqs.last_updated_by%type default null,
             i_last_updated_on  in patch_prereqs.last_updated_on%type default null
            ,i_raise_error IN VARCHAR2 DEFAULT 'N'
)
IS
   l_new_patch_prereqs  patch_prereqs%rowtype;
   l_old_patch_prereqs  patch_prereqs%rowtype;

BEGIN


  l_new_patch_prereqs := create_rec(
 i_patch_prereq_id,
 i_patch_name,
 i_prereq_patch,
 i_created_by,
 i_created_on,
 i_last_updated_by,
 i_last_updated_on
 );

  l_old_patch_prereqs := get_current_rec( i_patch_prereqs =>  l_new_patch_prereqs
                                       ,i_raise_error =>  i_raise_error);

  del(i_patch_prereqs => l_old_patch_prereqs);


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
    io_patch_prereqs  in out patch_prereqs%rowtype )
IS

BEGIN
  BEGIN
    --Insert
    patch_prereqs_tapi.ins( io_patch_prereqs => io_patch_prereqs );

  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN

      --update

      --Get primary key value for patch_prereqs


      io_patch_prereqs.patch_prereq_id := get_current_rec( i_patch_prereqs =>  io_patch_prereqs).patch_prereq_id;


      --Update
      patch_prereqs_tapi.upd( io_patch_prereqs => io_patch_prereqs );

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
             io_patch_prereq_id  in out patch_prereqs.patch_prereq_id%type,
             io_patch_name  in out patch_prereqs.patch_name%type,
             io_prereq_patch  in out patch_prereqs.prereq_patch%type,
             io_created_by  in out patch_prereqs.created_by%type,
             io_created_on  in out patch_prereqs.created_on%type,
             io_last_updated_by  in out patch_prereqs.last_updated_by%type,
             io_last_updated_on  in out patch_prereqs.last_updated_on%type
)
IS

   l_patch_prereqs             patch_prereqs%rowtype;

BEGIN

  l_patch_prereqs := create_rec(
 io_patch_prereq_id,
 io_patch_name,
 io_prereq_patch,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on
 );

  ins_upd(io_patch_prereqs => l_patch_prereqs);

  split_rec( l_patch_prereqs,
 io_patch_prereq_id,
 io_patch_name,
 io_prereq_patch,
 io_created_by,
 io_created_on,
 io_last_updated_by,
 io_last_updated_on
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
             i_patch_prereq_id  in patch_prereqs.patch_prereq_id%type default null,
             i_patch_name  in patch_prereqs.patch_name%type default null,
             i_prereq_patch  in patch_prereqs.prereq_patch%type default null,
             i_created_by  in patch_prereqs.created_by%type default null,
             i_created_on  in patch_prereqs.created_on%type default null,
             i_last_updated_by  in patch_prereqs.last_updated_by%type default null,
             i_last_updated_on  in patch_prereqs.last_updated_on%type default null
)
IS

BEGIN

  ins_opt(
             i_patch_prereq_id,
             i_patch_name,
             i_prereq_patch,
             i_created_by,
             i_created_on,
             i_last_updated_by,
             i_last_updated_on
 );

  EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
      --update
  upd_not_null(
             i_patch_prereq_id,
             i_patch_name,
             i_prereq_patch,
             i_created_by,
             i_created_on,
             i_last_updated_by,
             i_last_updated_on
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
RETURN BOOLEAN
IS
l_index1   PLS_INTEGER := i_collection1.FIRST;
l_index2   PLS_INTEGER := i_collection2.FIRST;
l_collections_equal     BOOLEAN     DEFAULT TRUE;

FUNCTION equal_records (
  rec1_in IN PATCH_PREREQS%ROWTYPE
, rec2_in IN PATCH_PREREQS%ROWTYPE
)
RETURN BOOLEAN
IS
  retval BOOLEAN;
BEGIN
  retval := rec1_in.PATCH_PREREQ_ID = rec2_in.PATCH_PREREQ_ID OR
     (rec1_in.PATCH_PREREQ_ID IS NULL AND rec2_in.PATCH_PREREQ_ID IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.PATCH_NAME = rec2_in.PATCH_NAME OR
     (rec1_in.PATCH_NAME IS NULL AND rec2_in.PATCH_NAME IS NULL);
  IF NOT NVL (retval, FALSE) THEN GOTO unequal_records; END IF;
  retval := rec1_in.PREREQ_PATCH = rec2_in.PREREQ_PATCH OR
     (rec1_in.PREREQ_PATCH IS NULL AND rec2_in.PREREQ_PATCH IS NULL);
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


END patch_prereqs_tapi;
/
