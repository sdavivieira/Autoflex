
describe('Raw Material Create - Quick Tests FIXED', () => {
    it('Check create page exists', () => {
        cy.visit('https://localhost:7193/RawMaterial/Create');

        cy.contains('h2', 'Criar Matéria-prima').should('exist');

        cy.get('input[name="Name"]').should('exist');
        cy.get('input[name="StockQuantity"]').should('exist');

        cy.contains('button', 'Salvar').should('exist');
        cy.contains('a', 'Voltar').should('exist');
    });

    it('Create a raw material', () => {
        cy.visit('https://localhost:7193/RawMaterial/Create');

        cy.get('input[name="Name"]').type('Copo Plástico ' + Date.now());
        cy.get('input[name="StockQuantity"]').type('50');
        cy.get('button[type="submit"]').click();

        cy.url().should('satisfy', (url) => {
            return url.includes('/RawMaterial') && !url.includes('/Create');
        });

        cy.get('table').should('exist');
        cy.get('h2').should('contain.text', 'Matérias-primas');
    });

    it('Go back to list', () => {
        cy.visit('https://localhost:7193/RawMaterial/Create');

        cy.contains('a', 'Voltar').click();

        cy.url().should('satisfy', (url) => {
            return url.includes('/RawMaterial') && !url.includes('/Create');
        });

        cy.get('table').should('exist');
    });
});