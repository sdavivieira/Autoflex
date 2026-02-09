
describe('Product List - Quick Tests', () => {
    it('Check products list page', () => {
        cy.visit('https://localhost:7193/Product/Index');

        cy.contains('h2', 'Produtos').should('exist');

        cy.get('table').should('exist');

        cy.contains('a', 'Novo Produto').should('exist');

        cy.get('table tbody tr').should('have.length.at.least', 1);
    });

    it('Navigate to edit from list', () => {
        cy.visit('https://localhost:7193/Product/Index');

        cy.get('table tbody tr').first().within(() => {
            cy.contains('a', 'Editar').click();
        });

        cy.url().should('include', '/Product/Edit/');
    });

    it('Navigate to create product', () => {
        cy.visit('https://localhost:7193/Product/Index');

        cy.contains('a', 'Novo Produto').click();

        cy.url().should('include', '/Product/Create');
    });
});