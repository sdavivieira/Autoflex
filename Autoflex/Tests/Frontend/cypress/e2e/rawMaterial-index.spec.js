
describe('Raw Material List - Quick Tests', () => {
    it('Check raw materials list page', () => {
        cy.visit('https://localhost:7193/RawMaterial');

        cy.contains('h2', 'Matérias-primas').should('exist');

        cy.get('table').should('exist');

        cy.contains('a', 'Nova Matéria-prima').should('exist');

        cy.get('table tbody tr').should('have.length.at.least', 1);
    });

    it('Navigate to edit from list', () => {
        cy.visit('https://localhost:7193/RawMaterial');

        cy.get('table tbody tr').first().within(() => {
            cy.contains('a', 'Editar').click();
        });

        cy.url().should('include', '/RawMaterial/Edit/');
    });

    it('Navigate to create raw material', () => {
        cy.visit('https://localhost:7193/RawMaterial');

        cy.contains('a', 'Nova Matéria-prima').click();

        cy.url().should('include', '/RawMaterial/Create');
    });
});