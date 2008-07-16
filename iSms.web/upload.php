<?php

require_once("dbdriver.php");

	function post_error( $err_string )
	{
		echo "error: " . $err_string;
	}

	function post_ok()
	{
		echo "ok";
	}
	
	function verify_user( $db, $username, $pwd )
	{
		$query = "SELECT uid FROM accounts WHERE username='".$username."' AND pwdmd5='" .$pwd. "'";
		$res = $db->query( $query );
		if( !$res ) return FALSE;
		
		if( pg_num_rows( $res ) == 0 ) return FALSE;
		$uid = pg_fetch_result( $res, 0, "uid" );
		return $uid;
	}

	if( !isset($_POST["username"]) || !isset( $_POST["password"] ) || $_POST["username"] == "" || $_POST["password"] == "" )
	{
		post_error( "Incorrect username or password!" );
		exit(-9);
	}
	
	$username 	= $_POST["username"];
	$password 	= $_POST["password"];
	
	$db = new dbDriver();
	if( !$db->open() ) 
	{
		post_error( "Could not connect to the database!" );		
		exit( -13 );
	}

	$uid = verify_user( $db, $username, $password );
	if( $uid == FALSE )
	{
		post_error( "Authentication failed!" );
		exit( -23 );
	}

	if( isset( $_POST["user_id"] ) && isset( $_POST["number"] ) )
	{
		$user_id 	= $_POST["user_id"];
		$number 	= $_POST["number"];
		
		foreach( $user_id as $i => $nuid  )
		{
			//echo $uid;
			//echo $number[$i];
			$query = "SELECT * FROM numbers WHERE number='".$number[$i]."'";
//			echo $query . "\n";
			$res = $db->query( $query );
			$fetch = pg_num_rows( $res );
//			echo "(fetched:".$fetch.")";
			if( $fetch != "0" ) continue;
		
			$query = "INSERT INTO numbers VALUES( '".$uid."', '".$number[$i]."', '".$nuid."' )";
//			echo $query . "\n";
			$db->query( $query );
		}
	}
	
	if( isset( $_POST["id"] ) && isset( $_POST["first"] ) && isset( $_POST["last"] ) )
	{
		$id 		= $_POST["id"];
		$first		= $_POST["first"];
		$last		= $_POST["last"];
		
		foreach( $id as $i => $ids  )
		{
			//echo $ids;
			//echo $first[$i];
			//echo $last[$i];
		
			$query = "SELECT * FROM users WHERE id='".$ids."'";
//			echo $query . "\n";
			$res = $db->query( $query );
			$fetch = pg_num_rows( $res );
//			echo "(fetched:".$fetch.")";
			if( $fetch != "0" ) continue;
		
			$query = "INSERT INTO users VALUES( '".$uid."', '".$ids."', '".$first[$i]."', '".$last[$i]."' )";
//			echo $query . "\n";
			$db->query( $query );
		}
	}
	
	if( isset( $_POST["md5"] )  && isset( $_POST["date"] )  && isset( $_POST["number"] )  && isset( $_POST["message"] )  && isset( $_POST["sent"] ) )
	{
		$md5 		= $_POST["md5"];
		$date		= $_POST["date"];
		$number 	= $_POST["number"];
		$message 	= $_POST["message"];
		$sent		= $_POST["sent"];

		foreach( $md5 as $i => $sum  )
		{
			//echo $sum;
			//echo $date[$i];
			//echo $number[$i];
			//echo $message[$i];
			//echo $sent[$i];

			$query = "SELECT * FROM messages WHERE md5='".$sum."'";
//			echo $query . "\n";
			$res = $db->query( $query );
			$fetch = pg_num_rows( $res );
//			echo "(fetched:".$fetch.")";
			if( $fetch != "0" ) continue;
		
			$query = "INSERT INTO messages VALUES( '".$uid."', '".$sum."', '".$number[$i]."', '".$date[$i]."', '".$message[$i]."', '".$sent[$i]."' )";
//			echo $query . "\n";
			$db->query( $query );
		}
	}
	
	$db->release();
	
	post_ok();

?>