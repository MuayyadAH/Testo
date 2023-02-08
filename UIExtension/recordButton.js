let button = document.getElementById('recButton');

button.classList.add('notRec');

button.addEventListener('click', () => {
	console.log('test');
	if (button.classList.contains('notRec')) {
		button.classList.remove('notRec');
		button.classList.add('Rec');
	}
	else {
		button.classList.remove('Rec');
		button.classList.add('notRec');
	}
});