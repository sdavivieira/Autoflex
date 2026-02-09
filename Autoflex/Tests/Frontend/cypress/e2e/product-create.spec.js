describe('Product Creation', () => {
    const baseUrl = 'https://localhost:7193'; // sem a barra final

    it('should create a new product', () => {
        cy.visit(`${baseUrl}/Product/Create`, { failOnStatusCode: false });

        cy.get('form').should('be.visible');

        cy.get('input[name="Name"]').type('Produto Teste Cypress');
        cy.get('input[name="Price"]').type('150');

        cy.get('button[type="submit"]').click();

        cy.url().should('include', '/Product');

        cy.contains('Produto Teste Cypress').should('exist');
    });
});
