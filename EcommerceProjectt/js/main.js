const inputs = document.querySelectorAll(".input");


function addcl(){
	let parent = this.parentNode.parentNode;
	parent.classList.add("focus");
}

function remcl(){
	let parent = this.parentNode.parentNode;
	if(this.value == ""){
		parent.classList.remove("focus");
	}
}


inputs.forEach(input => {
	input.addEventListener("focus", addcl);
	input.addEventListener("blur", remcl);
});

const redirect = document.getElementById("redirect").innerHTML;
function changeurl() {
	if (redirect === "Create Account...") {
		document.getElementById("redirect").href = "/Account/registration";

	}
	if (redirect === "Aready have Account") {
		document.getElementById("redirect").href = "/Account/login";
	}

	
}

$(document).ready(function () {

	$('.color-choose input').on('click', function () {
		var headphonesColor = $(this).attr('data-image');

		$('.active').removeClass('active');
		$('.left-column img[data-image = ' + headphonesColor + ']').addClass('active');
		$(this).addClass('active');
	});

});