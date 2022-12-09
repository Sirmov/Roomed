const allowedData = ['Type', 'OwnerId', 'NameInDocument', 'Country', 'DocumentNumber', 'PersonalNumber', 'PlaceOfBirth', 'Birthdate', 'IssuedBy', 'ValidFrom', 'ValidUntil'];
const storageKey = 'createIdentityDocument';
const createProfileLinkElement = document.getElementById('create-profile-link');
const identityDocumentForm = document.getElementById('identity-document-form');

if (sessionStorage.getItem(storageKey) !== null) {
    let data = JSON.parse(sessionStorage.getItem(storageKey));
    let inputs = document.querySelectorAll('input.form-control');

    inputs.forEach(i => {
        i.value = data[i.id];
    });

    sessionStorage.removeItem(storageKey);
}

createProfileLinkElement.addEventListener('click', (e) => {
    const formData = new FormData(identityDocumentForm);
    const formEntries = [...formData.entries()];
    let = data = formEntries.reduce((a, [k, v]) => (allowedData.includes(k) ? Object.assign(a, { [k]: v }) : a), {});
    sessionStorage.setItem(storageKey, JSON.stringify(data));
});
