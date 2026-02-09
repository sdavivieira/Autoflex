
describe('Production Suggestions Page', () => {
    beforeEach(() => {
        cy.visit('https://localhost:7193/Production/Suggestions');
    });

    it('Should load the production suggestions page', () => {
        cy.get('h2').should('contain.text', 'Produção sugerida');

        cy.get('table').should('exist');

        cy.get('table thead tr th').should('have.length', 3);
        cy.get('table thead tr th').eq(0).should('contain.text', 'Produto');
        cy.get('table thead tr th').eq(1).should('contain.text', 'Quantidade Produzível');
        cy.get('table thead tr th').eq(2).should('contain.text', 'Valor Total');
    });

    it('Should display production suggestions in table', () => {
        cy.get('table tbody tr').then($rows => {
            if ($rows.length > 0) {
                cy.get('table tbody tr').first().within(() => {
                    cy.get('td').eq(0).should('not.be.empty'); 
                    cy.get('td').eq(1).should('not.be.empty'); 
                    cy.get('td').eq(2).should('not.be.empty');
                });
            } else {
                cy.log('Nenhuma sugestão de produção disponível');
            }
        });
    });

    it('Should show valid data format', () => {
        cy.get('table tbody tr').each(($row, index) => {
            cy.wrap($row).within(() => {
                cy.get('td').eq(0).invoke('text').should('not.be.empty');
                cy.get('td').eq(1).invoke('text').should('match', /^\d+$/);
                cy.get('td').eq(2).invoke('text').should('match', /^[\d.,]+$/); 
            });

            if (index >= 4) return false;
        });
    });

    it('Should order by highest value first', () => {
        const values = [];

        cy.get('table tbody tr td:nth-child(3)').each(($td) => {
            const text = $td.text().trim();
            const value = parseFloat(text.replace(/[^\d.,]/g, '').replace(',', '.'));
            if (!isNaN(value)) {
                values.push(value);
            }
        }).then(() => {
            if (values.length > 1) {
                for (let i = 0; i < values.length - 1; i++) {
                    expect(values[i]).to.be.at.least(values[i + 1]);
                }
                cy.log(`Valores em ordem decrescente: ${values.join(', ')}`);
            }
        });
    });
});