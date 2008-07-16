<?php

require_once( "config.php" );

class dbDriver
{
	var $dblink;
	
	function dbDriver()
	{
		$this->dblink = FALSE;
	}
	
	function open()
	{
		global $host;
		global $port;
		global $base;
		global $user;
		global $pass;
	
		if( $this->dblink != FALSE ) return TRUE;
	
		$connstring = "host=".$host." port=".$port." dbname=".$base." user=".$user." password=".$pass;
		$this->dblink = pg_connect( $connstring );
		if( !$this->dblink ) return FALSE;
		
		return TRUE;
	}
	
	function release()
	{
		if( $this->dblink != FALSE )
		{
			pg_close( $this->dblink );
			$this->dblink = FALSE;
		}
	}
	
	function query( $query )
	{
		return pg_query( $this->dblink, $query );
	}
}

?>