	$("#btn-search").click(function(){
		$("#searchmsgwrap").hide();
		var hasError = false;
		var searchReg = /^[a-zA-Z0-9-\s]+$/;
		var searchVal = $("#searchterm").val();
		if(searchVal == '') {
			$("#searchmsgwrap").show();
			$("#searchmsg").text('Please enter a search text.');            
			hasError = true;
		} else if(!searchReg.test(searchVal)) {
			$("#searchmsgwrap").show();
			$("#searchmsg").text('Please enter a Valid text.');  
			hasError = true;
		}
		if(hasError == true) {return false;}
	});
