
  CREATE OR REPLACE EDITIONABLE PACKAGE "PATCH_INVOKER" authid current_user as
  --------------------------------------------------------------
  -- NAME   : patch_invoker
  -- PURPOSE: patch install routines that must run with invoker rights.
  -- $ Id: $
  --------------------------------------------------------------

  --------------------------------------------------------------
  -- do_sql_with_warning
  --------------------------------------------------------------
  PROCEDURE do_sql_with_warning(i_sql IN VARCHAR2);
  --------------------------------------------------------------
  -- do_sql_with_error
  --------------------------------------------------------------
  PROCEDURE do_sql_with_error(i_sql IN VARCHAR2);

  --------------------------------------------------------------
  -- compile_schema
  --------------------------------------------------------------
  PROCEDURE compile_schema(i_schema IN VARCHAR2 DEFAULT '%');

  --------------------------------------------------------------
  -- compile_post_patch
  --------------------------------------------------------------
  PROCEDURE compile_post_patch;

  --------------------------------------------------------------
  -- create_db_link
  --------------------------------------------------------------
  PROCEDURE create_db_link(i_schema       IN VARCHAR2
                          ,i_db_name      IN VARCHAR2
                          ,i_db_link_name IN VARCHAR2
                          ,i_password     IN VARCHAR2);

END;
/

--GRANTS


--SYNONYMS
