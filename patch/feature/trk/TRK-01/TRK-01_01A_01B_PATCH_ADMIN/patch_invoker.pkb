CREATE OR REPLACE PACKAGE BODY patch_invoker as
  --------------------------------------------------------------
  -- NAME   : patch_invoker
  -- PURPOSE: patch install routines that must run with invoker rights.
  -- $ Id: $
  --------------------------------------------------------------
 
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
      patch_installer.alert_warning;
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
      patch_installer.alert_error;
      put_lines(SQLERRM);
      put_lines('FAILURE.');
  END;
 
  --------------------------------------------------------------
  -- compile_schema
  --------------------------------------------------------------
  PROCEDURE compile_schema(i_schema IN VARCHAR2 DEFAULT '%') IS
 
  BEGIN
 
    dbms_output.enable(1000000);
 
    patch_installer.count_invalid_schema_objects(i_schema => i_schema);
 
    put_lines('Compiling schemas '||i_schema);
 
    --Recompile all objects in dependancy order.
    FOR l_object IN (  select * 
                       from  &&patch_admin_user..user_object_dependency_v 
                       where object_name <> 'patch_invoker' ) LOOP
 
     IF l_object.object_type = 'PACKAGE BODY' THEN
        do_sql_with_warning(i_sql => 'ALTER PACKAGE ' || l_object.owner||'.'||l_object.object_name || ' COMPILE BODY');
     ELSIF l_object.object_type = 'TYPE BODY' THEN
        do_sql_with_warning(i_sql => 'ALTER TYPE ' || l_object.owner||'.'||l_object.object_name || ' COMPILE BODY');
     ELSE
        do_sql_with_warning(i_sql => 'ALTER '||l_object.object_type||' ' || l_object.owner||'.'||l_object.object_name || ' COMPILE');      
     END IF;
 
    END LOOP;     
 
    put_lines('Remaining invalid objects in schema '||i_schema);
    patch_installer.list_invalid_schema_objects(i_schema => i_schema);
 
  END;
  
  --------------------------------------------------------------
  -- compile_post_patch
  --------------------------------------------------------------
  PROCEDURE compile_post_patch IS
 
  BEGIN
 
      IF patch_installer.f_error_count = 0 THEN
        compile_schema(i_schema  => patch_installer.f_patches().db_schema);
      END IF;
      
  END;
  
  --------------------------------------------------------------
  -- create_db_link
  --------------------------------------------------------------
  PROCEDURE create_db_link(i_schema       IN VARCHAR2
                          ,i_db_name      IN VARCHAR2
                          ,i_db_link_name IN VARCHAR2
                          ,i_password     IN VARCHAR2) IS
    l_command        VARCHAR2(2000);
  BEGIN
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
    
  END;
 
 
 
END patch_invoker;
/

