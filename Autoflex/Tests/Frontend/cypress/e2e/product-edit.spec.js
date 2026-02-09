
describe('Quick Product Edit Check', () => {
    it('Check if edit page works', () => {
        cy.visit('https://localhost:7193/Product/Edit/9');

        cy.get('#Name').should('exist');
        cy.get('#Price').should('exist');
        cy.get('#selectedRawMaterialId').should('exist');
        cy.get('#btnAddRawMaterial').should('exist');
        cy.get('#rawMaterialsGrid').should('exist');

        cy.contains('button', 'Salvar Produto').should('exist');

        cy.get('#Name').clear().type('Teste Rápido');

        cy.contains('button', 'Salvar Produto').click();

        cy.url().should('not.equal', 'https://localhost:7193/Product/Edit/9');
    });
});