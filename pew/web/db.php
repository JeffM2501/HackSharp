<?php  
	function DBConnect ( $host, $database, $user, $pass )
	{
		$db = mysql_pconnect($host,$user,$pass);
		if (!$db)
		{
			echo "unable to connect to db";
			exit;
		}
		else
			$result = mysql_select_db($database);
		return $db;
	}
	
	function DBError ()
	{
		return mysql_error();
	}
	
	function DBGet ( $query )
	{
		$result = mysql_query($query);
		if (!$result && $result != 0 && mysql_num_rows($result) > 0)
			return FALSE; 
			
		return $result;
	}
	
	function DBSet ( $query )
	{
		$result = mysql_query($query);
		if (!$result)
			return FALSE; 
			
		return TRUE;
	}
	
	function DBGetResultsForField ( $result, $field )
	{
		if (!$result)
			return FALSE;
			
		$list = array(); 
		$count = mysql_num_rows($result);
		for ($i = 0; $i < $count; $i += 1)
		{
			$row = mysql_fetch_array($result);
			$list[] = $row[$field];
		}
		
		return $list;
	}
	
	function DBQuery ( $db, $query )
	{			
		$results = DBGet($query);
		if (!$results)
			return FALSE;
			
		$list = array(); 
		$count = mysql_num_rows($results);
		for ($i = 0; $i < $count; $i += 1)
		{
			$r = array();
			$row = mysql_fetch_array($results);
			foreach ($row as $key=>$value)
			{
				if (!is_numeric($key))
					$r[$key] = $value;
					
			}
			$list[] = $r;
		}
		
		return $list;
	}
	
	function DBGetSingleField ( $keyName, $key, $table, $field )
	{
		$query = "SELECT " . $field . " FROM ". $table ." WHERE " . $keyName . "='" .$key . "'";
		$results = DBGetResultsForField(DBGet($query),$field );
		if (!$results)
			return FALSE;
		return Unsanitize($results[0]);
	}
	
	function DBSetSingleField ( $keyName, $key, $table, $field, $value )
	{
		$query = "UPDATE " . $table ." SET " . $field . "='" .$value."' WHERE " . $keyName ."='" .$key. "'";
		return DBSet($query); 
	}
	
	function Sanitize ( $value )
	{
		return mysql_real_escape_string(addslashes($value));	
	}
	
	function Unsanitize ( $value )
	{
		return stripslashes($value);	
	}
	
?>