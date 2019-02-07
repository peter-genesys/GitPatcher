
  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TEXT_MANIP" AS

--------------------------------------------------------------------------------
-- Name:   IFF
--------------------------------------------------------------------------------
  FUNCTION IFF(i_condition   in boolean
               ,i_string_if   in varchar2
               ,i_string_else in varchar2 DEFAULT NULL) RETURN varchar2 IS

  BEGIN
    IF i_condition THEN
      RETURN i_string_if;
    ELSE
      RETURN i_string_else;
    END IF;

  END IFF;

--------------------------------------------------------------------------------
-- Name:   SUBSTR2
--------------------------------------------------------------------------------

  FUNCTION SUBSTR2(p_string      IN VARCHAR2
                  ,p_start_pos   IN INTEGER
                  ,p_end_pos     IN INTEGER)
  --finds the sub string from start_pos to end_pos (inclusive)
  RETURN VARCHAR2
  IS
  BEGIN
    RETURN SUBSTR(p_string,p_start_pos,p_end_pos - p_start_pos + 1);
  END SUBSTR2;


  --------------------------------------------------------------------
  -- f_add
  --------------------------------------------------------------------

  FUNCTION f_add( i_string_a IN VARCHAR2
                 ,i_string_b IN VARCHAR2
                 ,i_delim    IN VARCHAR2 DEFAULT ' ') RETURN VARCHAR2 IS

  BEGIN

    IF i_string_a IS NOT NULL AND
       i_string_b IS NOT NULL THEN

      RETURN  i_string_a||i_delim||i_string_b;

    ELSE

      RETURN  i_string_a||i_string_b;

    END IF;

  END;

  --------------------------------------------------------------------
  --GENERIC PATTERN AND TEXT MANIPULATION
  --------------------------------------------------------------------

  --------------------------------------------------------------------
  -- F_REMOVE_FIRST_ELEMENT
  --
  --returns first element and remainder in io_list
  --------------------------------------------------------------------

FUNCTION f_remove_first_element(io_list     IN OUT VARCHAR2
                               ,i_delim     IN VARCHAR2 DEFAULT ' ') RETURN VARCHAR2
  IS
    l_first_delim_pos NUMBER;
    l_first_element   VARCHAR2(400);

  BEGIN
    io_list := LTRIM(io_list);
    --find first space
    l_first_delim_pos  := INSTR(io_list,i_delim);
    IF l_first_delim_pos = 0 THEN
      l_first_element := io_list;
      io_list  := NULL;
    ELSE
      --get first word
      l_first_element := SUBSTR(io_list,1,l_first_delim_pos-1);
      --get remainder
      io_list   := LTRIM(SUBSTR(io_list,  l_first_delim_pos+1));
    END IF;

    RETURN l_first_element;

  EXCEPTION
    WHEN OTHERS THEN
      RETURN 'Error';

  END f_remove_first_element;




  --------------------------------------------------------------------
  -- F_REMOVE_ELEMENTS
  --
  --returns i_element_count elements and remainder in io_list
  --------------------------------------------------------------------


  FUNCTION f_remove_elements(io_list         IN OUT VARCHAR2
                            ,i_element_count IN NUMBER
                            ,i_delim         IN VARCHAR2 DEFAULT ' ') RETURN VARCHAR2
  IS

    l_elements      VARCHAR2(400);
    l_element_count NUMBER := 0;

  BEGIN

    IF i_element_count > 0 THEN

      LOOP

        l_elements := f_add(l_elements
                           ,f_remove_first_element(io_list   => io_list
                                                  ,i_delim   => i_delim));

        l_element_count := l_element_count + 1;

        EXIT WHEN l_element_count = i_element_count OR
                  io_list IS NULL;

      END LOOP;


      IF l_element_count <> i_element_count THEN
        NULL;

      END IF;

    END IF;

    RETURN l_elements;

  END f_remove_elements;


  --------------------------------------------------------------------
  -- F_EXTRACT_ELEMENT
  --  (if i_element_pos is -1 will return the last element)
  -- returns i_element_pos element and remainder in io_list
  --------------------------------------------------------------------


  FUNCTION f_extract_element(io_list         IN OUT VARCHAR2
                            ,i_element_pos   IN NUMBER
                            ,i_delim         IN VARCHAR2 DEFAULT ' ') RETURN VARCHAR2
  IS

    l_new_elements  VARCHAR2(400);
    l_element_count NUMBER := 0;
    l_element       VARCHAR2(400);
    l_result        VARCHAR2(400);

  BEGIN

    IF i_element_pos = 0 THEN
      RETURN NULL;
    END IF;

    LOOP

      l_element := f_remove_first_element(io_list   => io_list
                                         ,i_delim   => i_delim);
      l_element_count := l_element_count + 1;

      IF l_element_count = i_element_pos OR io_list IS NULL THEN
        --keep this one
        l_result := l_element;
      ELSE
        l_new_elements := f_add(l_new_elements
                                ,l_element
                                ,i_delim   => i_delim);
      END IF;


      EXIT WHEN io_list IS NULL;

    END LOOP;

    io_list := l_new_elements;

    RETURN l_result;

  END;



  --------------------------------------------------------------------
  -- discard_first_element
  --
  -- remove and discard first element return remainder in io_list
  --------------------------------------------------------------------

  PROCEDURE discard_first_element(io_list   IN OUT VARCHAR2
                                 ,i_delim   IN     VARCHAR2 DEFAULT ' ' )
  IS

    l_first_element   VARCHAR2(400);

  BEGIN

    l_first_element := f_remove_first_element(io_list => io_list
                                             ,i_delim   => i_delim);

  END;

  --------------------------------------------------------------------
  -- F_EXTRACT_LAST_ELEMENT
  --
  -- extract and return last element return remainder in io_list
  --------------------------------------------------------------------

  FUNCTION f_extract_last_element(io_list   IN OUT VARCHAR2
                                 ,i_delim   IN     VARCHAR2 DEFAULT ' ' ) RETURN VARCHAR2
  IS

    l_last_element   VARCHAR2(400);

  BEGIN

    RETURN  f_extract_element(io_list       => io_list
                             ,i_element_pos => -1
                             ,i_delim   => i_delim);

  END;


  --------------------------------------------------------------------
  -- F_GET_LAST_ELEMENT
  --
  -- return last element
  --------------------------------------------------------------------

  FUNCTION f_get_last_element(i_list    IN  VARCHAR2
                             ,i_delim   IN  VARCHAR2 DEFAULT ' ' ) RETURN VARCHAR2
  IS

    l_last_element   VARCHAR2(400);
    l_list           VARCHAR2(400) := i_list;

  BEGIN

    RETURN  f_extract_last_element(io_list   => l_list
                                  ,i_delim   => i_delim) ;

  END;

  --------------------------------------------------------------------
  -- F_GET_ALL_BUT_LAST_ELEMENT
  --
  -- return all_but last element
  --------------------------------------------------------------------

  FUNCTION f_get_all_but_last_element(i_list    IN  VARCHAR2
                                     ,i_delim   IN  VARCHAR2 DEFAULT ' '  ) RETURN VARCHAR2
  IS

    l_last_element   VARCHAR2(400);
    l_list           VARCHAR2(400) := i_list;

  BEGIN

    l_last_element :=  f_extract_last_element(io_list => l_list
                                             ,i_delim   => i_delim) ;
    RETURN l_list;

  END;



    --------------------------------------------------------------------
    -- P_DISCARD_LAST_ELEMENT
    --
    -- extract and discard last element return remainder in io_list
    --------------------------------------------------------------------

    PROCEDURE p_discard_last_element(io_list   IN OUT VARCHAR2
                                    ,i_delim   IN     VARCHAR2 DEFAULT ' '  )
    IS

      l_last_element   VARCHAR2(400);

    BEGIN

      l_last_element :=  f_extract_last_element(io_list => io_list
                                               ,i_delim   => i_delim);

  END;


  --------------------------------------------------------------------
  -- F_GET_ELEMENT
  -- return the nth element
  --------------------------------------------------------------------

  FUNCTION f_get_element(i_string      IN VARCHAR2
                        ,i_element_pos IN NUMBER
                        ,i_delim       IN VARCHAR2 DEFAULT ' ' ) RETURN VARCHAR2
  IS


    l_string        VARCHAR2(400) := LTRIM(i_string);
    l_element       VARCHAR2(400);
    l_element_count INTEGER := 0;
    l_element_pos   INTEGER := i_element_pos;

  BEGIN

    WHILE l_element_count < l_element_pos AND l_string IS NOT NULL LOOP

      l_element := f_remove_first_element(io_list   => l_string
                                         ,i_delim   => i_delim);

      l_element_count := l_element_count + 1;

    END LOOP;

    IF l_element_count <> l_element_pos THEN
      --element not found
      l_element := NULL;
    END IF;

    RETURN l_element;

  END;

  --------------------------------------------------------------------
  -- f_get_first_element
  -- return the first element
  --------------------------------------------------------------------

  FUNCTION f_get_first_element(i_string      IN VARCHAR2
                              ,i_delim       IN VARCHAR2 DEFAULT ' ' ) RETURN VARCHAR2 IS

  BEGIN


    RETURN f_get_element(i_string       => i_string
                        ,i_element_pos  => 1
                        ,i_delim        => i_delim);

  END;

  --------------------------------------------------------------------
  -- f_get_first_csv
  -- return the first element separated by ,
  --------------------------------------------------------------------

  FUNCTION f_get_first_csv(i_string      IN VARCHAR2  ) RETURN VARCHAR2 IS

  BEGIN


    RETURN f_get_first_element(i_string    => i_string
                              ,i_delim     => ',');


  END;

----------------------------------------------------------------------------------------
-- strip_chars
----------------------------------------------------------------------------------------

   function strip_chars ( i_string  IN VARCHAR2
                        , i_keep    IN VARCHAR2
                        , i_strip   IN VARCHAR2) RETURN VARCHAR2 is

   begin

     RETURN TRANSLATE(i_string,i_keep||i_strip,i_keep); -- strip punctuation

   end;


END text_manip;
/
