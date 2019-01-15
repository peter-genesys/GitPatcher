
  CREATE OR REPLACE EDITIONABLE PROCEDURE "CREATE_DB_LINK" (
    i_schema       IN VARCHAR2
   ,i_db_name      IN VARCHAR2
   ,i_db_link_name IN VARCHAR2
   ,i_password     IN VARCHAR2) authid current_user as

    l_command        VARCHAR2(2000);

---------------------------------------------------------------------------
--dbms_output feedback.
---------------------------------------------------------------------------

PROCEDURE put_lines(i_line IN VARCHAR2 ) IS
--Divide i_line into a number of 100 char lines for easy display and output each.

  l_line VARCHAR2(32000)     := i_line;
  G_LINESIZE CONSTANT NUMBER := 100;
BEGIN
  WHILE l_line IS NOT NULL LOOP                        --Loop until l_line is empty.
    dbms_output.put_line(SUBSTR(l_line,1,G_LINESIZE)); --output left-most chars (upto G_LINESIZE chars)
    l_line := SUBSTR(l_line,G_LINESIZE+1);             --shorten l_line by G_LINESIZE chars from the left.
  END LOOP;

END;

  --------------------------------------------------------------
  -- do_sql_with_warning
  --------------------------------------------------------------
  PROCEDURE do_sql_with_warning(i_sql IN VARCHAR2) IS
  BEGIN
    put_lines(i_sql);
    EXECUTE IMMEDIATE i_sql;
    put_lines('SUCCESS.');
  EXCEPTION
    WHEN OTHERS THEN
      --patch_installer.alert_warning;
      put_lines(SQLERRM);
      put_lines('NOT DONE.');
  END;
  --------------------------------------------------------------
  -- do_sql_with_error
  --------------------------------------------------------------
  PROCEDURE do_sql_with_error(i_sql IN VARCHAR2) IS
  BEGIN
    put_lines(i_sql);
    EXECUTE IMMEDIATE i_sql;
    put_lines('SUCCESS.');
  EXCEPTION
    WHEN OTHERS THEN
      --patch_installer.alert_error;
      put_lines(SQLERRM);
      put_lines('FAILURE.');
  END;



  BEGIN --MAIN
    dbms_output.enable(1000000);

    IF i_db_name IS NOT NULL THEN
      l_command := 'DROP DATABASE LINK ' || i_db_link_name;
      --dbms_output.put_line(l_command);
      do_sql_with_warning(l_command);

      l_command := 'CREATE DATABASE LINK "' || i_db_link_name
              || '" CONNECT TO ' || i_schema
              || ' identified by "'||i_password
              ||'" USING ''' || i_db_name || '''';
      --dbms_output.put_line(l_command);
      do_sql_with_error(l_command);

    END IF;


END create_db_link;
/

--GRANTS


--SYNONYMS
