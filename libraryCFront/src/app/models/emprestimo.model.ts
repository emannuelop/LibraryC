export class Emprestimo {
    idEmprestimo!: number;
    idLivro!: number;
    idCliente!: number;
    dataEmprestimo!: Date;
    dataDevolucao!: Date;
    dataPrevistaDevolucao!: Date;
    status!: string;
}