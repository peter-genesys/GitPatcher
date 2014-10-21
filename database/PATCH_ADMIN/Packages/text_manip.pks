CREATE OR REPLACE PACKAGE text_manip AS 
 
--------------------------------------------------------------------------------
-- Name:   IFF
--------------------------------------------------------------------------------
  FUNCTION IFF(i_condition   in boolean 
               ,i_string_if   in varchar2
               ,i_string_else in varchar2 DEFAULT NULL) RETURN varchar2;
  
--------------------------------------------------------------------------------
-- Name:   SUBSTR2
--------------------------------------------------------------------------------
  
  FUNCTION SUBSTR2(p_string      IN VARCHAR2
                  ,p_start_pos   IN INTEGER
                  ,p_end_pos     IN INTEGER)
  --finds the sub string from start_pos to end_pos (inclusive)
  RETURN VARCHAR2;
  
  --------------------------------------------------------------------  
  --GENERIC PATTERN AND TEXT MANIPULATION
  --------------------------------------------------------------------  
 
  --------------------------------------------------------------------
  -- F_REMOVE_FIRST_ELEMENT
  --
  --returns first element and remainder in io_list
  --------------------------------------------------------------------

FUNCTION f_remove_first_element(io_list     IN OUT VARCHAR2
                               ,i_delim     IN VARCHAR2 DEFAULT ' ') RETURN VARCHAR2;

  --------------------------------------------------------------------
  -- F_REMOVE_ELEMENTS
  --
  --returns i_element_count elements and remainder in io_list
  --------------------------------------------------------------------


  FUNCTION f_remove_elements(io_list         IN OUT VARCHAR2
                            ,i_element_count IN NUMBER
                            ,i_delim         IN VARCHAR2 DEFAULT ' ') RETURN VARCHAR2;
  --------------------------------------------------------------------
  -- F_EXTRACT_ELEMENT
  --
  --returns i_element_pos element and remainder in io_list
  --------------------------------------------------------------------


  FUNCTION f_extract_element(io_list         IN OUT VARCHAR2
                            ,i_element_pos   IN NUMBER
                            ,i_delim         IN VARCHAR2 DEFAULT ' ') RETURN VARCHAR2;


  --------------------------------------------------------------------
  -- discard_first_element
  --
  -- remove and discard first element return remainder in io_list
  --------------------------------------------------------------------

  PROCEDURE discard_first_element(io_list   IN OUT VARCHAR2
                                 ,i_delim   IN     VARCHAR2 DEFAULT ' ' );

  --------------------------------------------------------------------
  -- F_EXTRACT_LAST_ELEMENT
  --
  -- extract and return last element return remainder in io_list
  --------------------------------------------------------------------

  FUNCTION f_extract_last_element(io_list   IN OUT VARCHAR2
                                 ,i_delim   IN     VARCHAR2 DEFAULT ' ' ) RETURN VARCHAR2;

  --------------------------------------------------------------------
  -- F_GET_LAST_ELEMENT
  --
  -- return last element
  --------------------------------------------------------------------

  FUNCTION f_get_last_element(i_list    IN  VARCHAR2 
                             ,i_delim   IN  VARCHAR2 DEFAULT ' ' ) RETURN VARCHAR2;

  --------------------------------------------------------------------
  -- F_GET_ALL_BUT_LAST_ELEMENT
  --
  -- return all_but last element
  --------------------------------------------------------------------

  FUNCTION f_get_all_but_last_element(i_list    IN  VARCHAR2
                                     ,i_delim   IN  VARCHAR2 DEFAULT ' '  ) RETURN VARCHAR2;

    --------------------------------------------------------------------
    -- P_DISCARD_LAST_ELEMENT
    --
    -- extract and discard last element return remainder in io_list
    --------------------------------------------------------------------

    PROCEDURE p_discard_last_element(io_list   IN OUT VARCHAR2
                                    ,i_delim   IN     VARCHAR2 DEFAULT ' '  );
  --------------------------------------------------------------------
  -- F_GET_ELEMENT
  -- return the nth element
  --------------------------------------------------------------------

  FUNCTION f_get_element(i_string      IN VARCHAR2
                        ,i_element_pos IN NUMBER
                        ,i_delim       IN VARCHAR2 DEFAULT ' ' ) RETURN VARCHAR2;
  
  --------------------------------------------------------------------
  -- f_get_first_element
  -- return the first element
  --------------------------------------------------------------------

  FUNCTION f_get_first_element(i_string      IN VARCHAR2
                              ,i_delim       IN VARCHAR2 DEFAULT ' ' ) RETURN VARCHAR2;
  
  --------------------------------------------------------------------
  -- f_get_first_csv
  -- return the first element separated by ,
  --------------------------------------------------------------------

  FUNCTION f_get_first_csv(i_string      IN VARCHAR2  ) RETURN VARCHAR2;
 
 
----------------------------------------------------------------------------------------
-- strip_chars
----------------------------------------------------------------------------------------
  
   function strip_chars ( i_string  IN VARCHAR2
                        , i_keep    IN VARCHAR2
                        , i_strip   IN VARCHAR2) RETURN VARCHAR2;
END text_manip;
/
 