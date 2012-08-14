/* contact form */
$(document).ready(function(){
	var form = $(".ef-contact");
	var name = $(".ef-name");
	var email = $(".ef-email");
	var message = $(".ef-message");
	
	name.blur(validateName);
	email.blur(validateEmail);
	message.blur(validateMessage);
	
	var inputs = form.find(":input").filter(":not(:submit)").filter(":not(:checkbox)").filter(":not([type=hidden])").filter(":not([validate=false])");

	form.submit(function(){
		if(validateName() & validateEmail() & validateMessage()){
			
			var $name = name.val();
			var $email = email.val();
			var $message = message.val();
			$.ajax({
				type: 'POST',
				url: "get_mail.php",
				data: form.serialize(),
				success: function(ajaxCevap) {
					$('.qcontact .ef-list').hide();
					$('.qcontact .ef-list').html(ajaxCevap);
					$('.qcontact .ef-list').fadeIn("normal")
					.delay(2000).fadeOut().prev().find('.ef-name,.ef-email,.ef-website').val('');
					name.attr("value", "");
					email.attr("value", "");
					message.attr("value", "");
				}
			});

			return false;
		}else{
			return false;
		}
	});
	
	function validateEmail(){
		var a = email.val();
		var filter = /^[a-zA-Z0-9_\.\-]+\@([a-zA-Z0-9\-]+\.)+[a-zA-Z0-9]{2,4}$/;
		if(filter.test(a)){
			email.removeClass("not-valid").addClass("valid");
			return true;
		}
		else{
			email.addClass("not-valid").removeClass("valid");
			return false;
		}
	}

	function validateName(){
		if(name.val() == 'Name'){
			name.addClass("not-valid").removeClass("valid");
			return false;
		}
		else{
			name.removeClass("not-valid").addClass("valid");
			return true;
		}
	}
	
	function validateMessage(){
		if(!message.val()){
			message.addClass("not-valid").removeClass("valid");
			return false;
		}else{			
			message.removeClass("not-valid").addClass("valid");
			return true;
		}
	}
		
});
/* end contact form */