﻿using Class_Management_System.Enums;
using Class_Management_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace Class_Management_System.Structures
{
    public class Grafo : IGrafo
    {
        private List<Vertice> vertices;
        private List<Aresta> arestas;
        private int componentes; //Número de componentes do grafo

        public int Numero_vertices { get { return this.vertices.Count; } }

        public Grafo()
        {
            this.Init();
        }

        public Grafo(int numero_vertices)
        {
            this.Init();
            this.vertices = new List<Vertice>(numero_vertices);
        }

        public Grafo(List<Vertice> lstVertices)
        {
            this.Init();
            this.vertices = lstVertices;
            this.BuscarArestasNosVertices();
        }

        public Grafo(List<Vertice> vertices, List<Aresta> arestas)
        {
            this.Init();
            this.OrganizarVerticesEArestas(vertices, arestas);
        }

        public List<Vertice> GetVertices()
        {
            return this.vertices;
        }

        /// <summary>
        /// Percorre todos os vértices do grafo coletando suas arestas
        /// (não duplica aresta)
        /// </summary>
        /// <returns></returns>
        public List<Aresta> GetArestas()
        {
            List<Aresta> arestas = new List<Aresta>();
            this.vertices.ForEach(vertice =>
            {
                vertice.GetArestas().ForEach(aresta =>
                {
                    if (!arestas.Contains(aresta)) arestas.Add(aresta);
                });
            });
            return this.arestas;
        }

        /// <summary>
        /// Retorna um vértice do grafo baseado no indice
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vertice GetVertice(int index)
        {
            if (index >= 0 && index < this.vertices.Count - 1) return this.vertices[index];
            else throw new Exception("Indice informado é maior que o tamanho total da lista de vértices");
        }

        /// <summary>
        /// Busca um vértice de acordo com o valor informado no parâmetro
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public Vertice GetVertice(string valor)
        {
            if (valor == null) return null;
            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.GetDadoValor().Equals(int.Parse(valor))) return vertice;
            }
            return null;
        }

        /// <summary>
        /// Retorna uma aresta do grafo baseado no indice
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Aresta GetAresta(int index)
        {
            if (index >= 0 && index < this.arestas.Count - 1) return this.arestas[index];
            else throw new Exception("Indice informado é maior que o tamanho total da lista de arestas");
        }

        /// <summary>
        /// Define a variável "visitado" de cada vértice como false
        /// </summary>
        public void LimpaVisitaVertices()
        {
            foreach (Vertice vAux in vertices)
            {
                vAux.SetVisitado(false);
                this.CalcularArestas(vAux);
            }
        }

        /// <summary>
        /// Instâncializa as variáveis "componente", "vertices" e "arestas"
        /// </summary>
        private void Init()
        {
            this.componentes = 0;
            this.vertices = new List<Vertice>();
            this.arestas = new List<Aresta>();
        }

        /// <summary>
        /// Aumenta o números de arestas que o grafo possui contando quantas arestas 
        /// o vértice passado no parâmetro possui
        /// </summary>
        /// <param name="vertice"></param>
        private int CalcularArestas(Vertice vertice)
        {
            int num_arestas = 0;
            return num_arestas += vertice.GetArestas().Count;
        }

        /// <summary>
        /// Aumenta o números de arestas que o grafo possui contando quantas arestas 
        /// a lista de vértices passado no parâmetro possui
        /// </summary>
        /// <param name="vertice"></param>
        private int CalcularArestas(List<Vertice> vertices)
        {
            int num_arestas = 0;
            foreach (Vertice vertice in vertices)
            {
                num_arestas += vertice.GetArestas().Count;
            }
            return num_arestas;
        }

        /// <summary>
        /// Procura e retorna pelo primeiro vértice cujo dado é igual aquele passado no parâmetro.
        /// Retorna null se nenhum for encontrado
        /// </summary>
        /// <param name="dado"></param>
        /// <returns></returns>
        public Vertice GetVertice(IDado dado)
        {
            if (dado != null)
            {
                foreach (Vertice vertice in this.vertices)
                {
                    if (vertice.GetDado().Equals(dado))
                    {
                        return vertice;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Verifica se existe o vértice no grafo
        /// </summary>
        /// <param name="vertice"></param>
        /// <returns></returns>
        public bool Contem(IDado vertice)
        {
            if (vertice != null)
            {
                foreach (Vertice verticeLocal in this.vertices)
                {
                    if (verticeLocal.GetDado().Equals(vertice))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se existe o vértice no grafo, fazendo a comparação pelo
        /// método Equals do vértice
        /// </summary>
        /// <param name="vertice"></param>
        /// <returns></returns>
        public bool Contem(Vertice vertice)
        {
            if (vertice != null)
            {
                foreach (Vertice verticeLocal in this.vertices)
                {
                    if (vertice.GetDado().Equals(verticeLocal.GetDado()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se o grafo possui uma aresta, fazendo a comparação pelo método
        /// Equals da aresta.
        /// </summary>
        /// <param name="aresta"></param>
        /// <returns></returns>
        public bool Contem(Aresta aresta)
        {
            if (aresta != null)
            {
                foreach (Aresta arestaLocal in this.arestas)
                {
                    if (aresta.Equals(arestaLocal))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Insere um novo vértice ao grafo
        /// </summary>
        /// <param name="v1"></param>
        public void AddVertice(Vertice v1)
        {
            if (v1 != null)
            {
                this.vertices.Add(v1);
                this.CalcularArestas(v1);
                this.AddArestasEmAdjacentes(v1);

                foreach (Aresta a in v1.GetArestas())
                {
                    if (!this.Contem(a)) this.arestas.Add(a);
                }
            }
        }

        /// <summary>
        /// Insere uma lista de vértices no grafo
        /// </summary>
        /// <param name="v1"></param>
        public void AddVertice(List<Vertice> v1)
        {
            if (v1 != null)
            {
                v1.ForEach(vertice =>
                {
                    this.vertices.Add(vertice);
                    this.CalcularArestas(vertice);
                    this.AddArestasEmAdjacentes(vertice);

                    foreach (Aresta a in vertice.GetArestas())
                    {
                        if (!this.Contem(a)) this.arestas.Add(a);
                    }
                });
            }
        }

        /// <summary>
        /// Insere um novo vértice no grafo SE ele já não foi adicionado
        /// </summary>
        /// <param name="v1"></param>
        public void AddVerticeSeNaoContem(Vertice v1)
        {
            if (v1 != null && !this.Contem(v1))
            {
                this.vertices.Add(v1);
                this.CalcularArestas(v1);
                this.AddArestasEmAdjacentes(v1);

                foreach (Aresta a in v1.GetArestas())
                {
                    if (!this.Contem(a)) this.arestas.Add(a);
                }
            }
        }

        /// <summary>
        /// Adiciona as arestas de um vértice nos outro vértice 
        /// no qual ela está ligada, SE este não tiver ela adicionada
        /// em sua lista
        /// </summary>
        public void AddArestasEmAdjacentes(Vertice vertice)
        {
            Vertice vertice1;
            foreach (Aresta aresta in vertice.GetArestas())
            {
                vertice1 = aresta.GetVerticeDiferente(vertice);
                if (!vertice1.Contem(aresta))
                {
                    vertice1.AddAresta(aresta);
                }
            }
        }

        /// <summary>
        /// Insere uma nova aresta para os vértices aos quais ela está ligada.
        /// Se os vértices não estão no grafo, eles são inseridos no grafo.
        /// </summary>
        /// <param name="aresta"></param>
        public void AddAresta(Aresta aresta)
        {
            if (aresta != null && aresta.getVertice1 != null && aresta.getVertice2 != null)
            {
                Vertice v1, v2;

                v1 = aresta.getVertice1;
                v2 = aresta.getVertice2;

                if (!this.Contem(v1)) this.vertices.Add(v1);
                if (!this.Contem(v2)) this.vertices.Add(v2);

                if (!v1.Contem(aresta)) v1.AddAresta(aresta);
                if (!v2.Contem(aresta)) v2.AddAresta(aresta);
                this.arestas.Add(aresta);
            }
        }

        /// <summary>
        /// Retira um vertice do grafo e define o objeto como nulo
        /// </summary>
        /// <param name="v1"></param>
        public void RemoverVertice(Vertice v1)
        {
            if (v1 != null && this.Contem(v1))
            {
                this.vertices.Remove(v1);
                this.LimparArestas(v1);
                v1 = null;
            }
        }

        public void RemoverAresta(Aresta a)
        {
            if (a != null && this.Contem(a))
            {
                a.getVertice1.RemoverAresta(a);
                a.getVertice2.RemoverAresta(a);

                //foreach (Vertice v in this.GetVertices())
                //{
                //    if (v.Contem(a))
                //    {
                //        Vertice outr = a.GetVerticeDiferente(v);
                //        v.RemoverAresta(a);

                //    }
                //        v.RemoverAresta(a);
                //}

                this.arestas.Remove(a);
                a = null;
            }
        }

        /// <summary>
        /// Remove os valores nulos da lista de arestas dos vértices que fazem ligação com aquele passado no parâmetro.
        /// </summary>
        /// <param name="v1"></param>
        private void LimparArestas(Vertice v1)
        {
            if (v1 != null)
            {
                Vertice vertice1;
                Vertice vertice2;
                List<Vertice> verticesLimpar = new List<Vertice>();

                foreach (Aresta aresta in v1.GetArestas())
                {
                    vertice1 = aresta.GetVertices()[0];
                    vertice2 = aresta.GetVertices()[1];

                    if (!vertice1.GetDadoValor().Equals(v1.GetDadoValor()))
                    {
                        vertice1.RemoverAresta(aresta);
                    }
                    else if (!vertice2.GetDadoValor().Equals(v1.GetDadoValor()))
                    {
                        vertice2.RemoverAresta(aresta);
                    }
                }
            }
        }

        public IGrafo GetAGMKruskal(out StringBuilder ordemInsercao)
        {
            Aresta arestaMenor = null;
            Vertice v1, v2;
            Grafo retorno = new Grafo();
            List<Aresta> arestas = this.GetArestas();
            ordemInsercao = new StringBuilder();

            do
            {
                arestaMenor = this.GetMenorArestaDesempate(this.GetArestasMenorPeso(arestas));
                if (arestaMenor != null)
                {
                    arestas.Remove(arestaMenor);
                    v1 = arestaMenor.getVertice1;
                    v2 = arestaMenor.getVertice2;

                    if (!v1.GetVerticeChefe().Equals(v2.GetVerticeChefe()))
                    {
                        if (v1.FoiVisitado()) v2.SetVerticeChefe(v1.GetVerticeChefe());
                        else v1.SetVerticeChefe(v2.GetVerticeChefe());

                        v1.SetVisitado(true);
                        v2.SetVisitado(true);

                        retorno.AddAresta(arestaMenor);
                        ordemInsercao.Append(arestaMenor.getValorVertice1 + "-" + arestaMenor.getValorVertice2 + " ");
                    }
                }
            } while (arestaMenor != null);
            this.ResetarVisitaVertices();
            return retorno;
        }

        /// <summary>
        /// Define a visita de todos os vértices do grafo como FALSE
        /// </summary>
        private void ResetarVisitaVertices()
        {
            this.vertices.ForEach(vertice => vertice.SetVisitado(false));
        }

        public IGrafo GetAGMKruskal(Vertice inicial, out StringBuilder ordemInsercaoVertices) // ~ pra que vértice inicial???
        {
            if (!inicial.Contem(GetMenorArestaDesempate(GetArestasMenorPeso(this.arestas))))
            {
                throw new Exception("Vértice passado por parâmetro não pode ser o vértice inicial da ordem de inserção pois não contém a menor aresta do grafo.");
            }
            return GetAGMKruskal(out ordemInsercaoVertices);
        }

        /// <summary>
        /// Retorna uma lista com as aresta de menor peso do grafo.
        /// Caso duas ou mais arestas possuam o mesmo peso, sendo elas as menores,
        /// ambas são retornadas.
        /// </summary>
        /// <param name="arestas"></param>
        /// <returns></returns>
        private List<Aresta> GetArestasMenorPeso(List<Aresta> arestas)
        {
            int menorPeso = int.MaxValue;
            List<Aresta> empateMenorPeso = new List<Aresta>();

            foreach (Aresta aresta in arestas) // descobre o menor peso
                menorPeso = aresta.GetPeso() < menorPeso ? aresta.GetPeso() : menorPeso;

            foreach (Aresta aresta in arestas) // separa as arestas com o menor peso
            {
                if (aresta.GetPeso() == menorPeso)
                    empateMenorPeso.Add(aresta);
            }

            return empateMenorPeso;
        }

        /// <summary>
        /// Retorna uma lista com as aresta de menor peso do grafo.
        /// Caso duas ou mais arestas possuam o mesmo peso, sendo elas as menores,
        /// ambas são retornadas.
        /// </summary>
        /// <param name="arestas"></param>
        /// <returns></returns>
        private List<Aresta> GetArestasMenorPeso()
        {
            int menorPeso = int.MaxValue;
            List<Aresta> arestas = this.GetArestas();
            arestas.ForEach(aresta => menorPeso = aresta.GetPeso() < menorPeso ? aresta.GetPeso() : menorPeso);
            return arestas.FindAll(aresta => aresta.GetPeso() == menorPeso); ;
        }

        private Aresta GetMenorArestaDesempate(List<Aresta> arestas)
        {
            if (arestas.Count == 1) return arestas[0];

            int[] somaIndice = new int[arestas.Count];
            for (int i = 0; i < somaIndice.Length; i++) // verifica a soma dos números (índices) dos vértices de cada aresta
                somaIndice[i] = (int)arestas[i].getValorVertice1 + (int)arestas[i].getValorVertice2; // ~~~~~ ver sobre Dado

            int menorSoma = int.MaxValue;
            for (int i = 0; i < somaIndice.Length; i++) // descobre a menor soma
                menorSoma = somaIndice[i] < menorSoma ? somaIndice[i] : menorSoma;

            List<Aresta> segundoEmpate = new List<Aresta>();

            for (int i = 0; i < somaIndice.Length; i++) // adiciona a aresta de menor soma na lista de segundo empate
            {
                if (menorSoma == somaIndice[i])
                    segundoEmpate.Add(arestas[i]);
            }

            if (segundoEmpate.Count == 1) // se não houve segundo empate
                return segundoEmpate[0];

            int menorIndice = int.MaxValue;
            Aresta arestaMenorIndice = null;
            for (int i = 0; i < segundoEmpate.Count; i++) // descobre a aresta de vértice de menor número (índice)
            {
                if ((int)segundoEmpate[i].getValorVertice1 < menorIndice)
                {
                    menorIndice = (int)segundoEmpate[i].getValorVertice1;
                    arestaMenorIndice = segundoEmpate[i];
                }

                if ((int)segundoEmpate[i].getValorVertice2 < menorIndice)
                {
                    menorIndice = (int)segundoEmpate[i].getValorVertice2;
                    arestaMenorIndice = segundoEmpate[i];
                }
            }

            return arestaMenorIndice;
        }

        /// <summary>
        /// Gera um grafo pelo método Prim iniciando pelo vértice passado 
        /// por referência(caso ele exista no grafo)
        /// </summary>
        /// <returns></returns>
        public IGrafo GetAGMPrim(Vertice v1, out StringBuilder ordemInsercaoVertices)
        {
            ordemInsercaoVertices = new StringBuilder();
            Grafo AGM = new Grafo();
            Grafo clone = this.Clonar();
            Aresta proxima;

            v1 = Vertice.Get(v1, clone.GetVertices());
            AGM.AddVertice(v1);

            while (AGM.Numero_vertices < clone.Numero_vertices)
            {
                proxima = this.GetMenorArestaDesempate(this.GetArestasMenorPeso(AGM.GetArestasNaoVisitadas()));

                if (AGM.Contem(proxima.getVertice1) && AGM.Contem(proxima.getVertice2))
                {
                    AGM.RemoverAresta(proxima);
                }
                else if (AGM.Contem(proxima.getVertice1))
                {
                    AGM.AddVertice(proxima.getVertice2); //adiciona o vértice
                    ordemInsercaoVertices.Append(proxima.getValorVertice1 + "-" + proxima.getValorVertice2 + " "); // adiciona os vertices a lista
                    proxima.SetVisitado(true);
                }
                else
                {
                    AGM.AddVertice(proxima.getVertice1); //adiciona o vértice
                    ordemInsercaoVertices.Append(proxima.getValorVertice2 + "-" + proxima.getValorVertice1 + " "); // adiciona os vertices a lista
                    proxima.SetVisitado(true);
                }
            }

            AGM.GetArestasNaoVisitadas().ForEach(aresta => AGM.RemoverAresta(aresta));
            return AGM;
        }

        /// <summary>
        /// Gera um grafo pelo método Prim iniciando pelo primeiro vértice
        /// do grafo
        /// </summary>
        /// <returns></returns>
        public IGrafo GetAGMPrim(out StringBuilder ordemInsercaoVertices)
        {
            return this.GetAGMPrim(this.vertices[0], out ordemInsercaoVertices);
        }

        /// <summary>
        /// Retorna uma lista com todas as arestas do grafo que não foram visitadas
        /// </summary>
        /// <returns></returns>
        public List<Aresta> GetArestasNaoVisitadas()
        {
            return this.GetArestas().FindAll(aresta => !aresta.FoiVisitado());
        }

        /// <summary>
        /// Retorna uma lista com todas as arestas do grafo que não foram visitadas
        /// </summary>
        /// <returns></returns>
        public List<Aresta> GetArestasVisitadas()
        {
            List<Aresta> retorno = new List<Aresta>();

            foreach (Aresta a in this.arestas)
            {
                if (a.FoiVisitado()) retorno.Add(a);
            }

            return retorno;
        }

        /// <summary>
        /// Retorna um grafo que é a "imagem" restante para o grafo original ser completo
        /// </summary>
        /// <returns></returns>
        public Grafo GetComplementar()
        {
            List<Vertice> vertices = new List<Vertice>();
            Vertice v1 = null, v2 = null;
            Aresta aresta;

            this.vertices.ForEach(vertice =>
            {
                this.vertices.ForEach(vertice2 =>
                {
                    if (!vertice.Equals(vertice2) && !vertice.TemArestaComVertice(vertice2))
                    {
                        v1 = Vertice.Get(vertice, vertices);
                        v2 = Vertice.Get(vertice2, vertices);

                        if (v1 == null)
                        {
                            v1 = new Vertice(vertice.GetDado());
                            vertices.Add(v1);
                        }
                        if (v2 == null)
                        {
                            v2 = new Vertice(vertice2.GetDado());
                            vertices.Add(v2);
                        }

                        aresta = new Aresta(v1, v2, 10);

                        v1.AddAresta(aresta);
                        v2.AddAresta(aresta);
                    }
                });
            });
            return new Grafo(vertices);
        }

        /// <summary>
        /// Retorna o número de vértices que, se removidos criam novos componentes no grafo.
        /// </summary>
        public int GetCutVertices()
        {
            int count = 0, posicao = 0, n_vertices = this.vertices.Count;
            int n_componentes = this.Componentes();
            Vertice removido;

            while (posicao != n_vertices)
            {
                removido = this.vertices[0];
                this.RemoverVertice(this.vertices[0]);

                if (this.GetComponentes() != n_componentes) count++;
                this.AddVertice(removido);
                posicao++;
            }
            return count;
        }

        /// <summary>
        /// Faz um clone em cascada do Grafo. ou seja, cria uma cópia do grafo e de todas as suas
        /// arestas e vértices
        /// </summary>
        /// <returns></returns>
        private Grafo Clonar()
        {
            List<Vertice> vertices = new List<Vertice>();
            this.vertices.ForEach(vertice => this.CloneUtil(vertices, vertice));
            this.ResetarVisitaArestas();
            return new Grafo(vertices);
        }

        /// <summary>
        /// Método recursivo para ser usado no Clonar()
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="vertice"></param>
        private void CloneUtil(List<Vertice> vertices, Vertice vertice)
        {
            Vertice v_clone, retorno;
            Aresta a_clone;

            if (!Vertice.Contem(vertices, vertice))
            {
                v_clone = vertice.Clonar();

                foreach (Aresta aresta in v_clone.GetArestas())
                {
                    if (!aresta.FoiVisitado())
                    {
                        a_clone = (Aresta)aresta.Clone();
                        a_clone.SetVisitado(true);

                        aresta.getVertice1.SetAresta(aresta.getVertice1.getIndexAresta(aresta), a_clone);
                        aresta.getVertice2.SetAresta(aresta.getVertice2.getIndexAresta(aresta), a_clone);
                    }
                    else
                    {
                        a_clone = aresta;
                    }

                    if (a_clone.getValorVertice1.Equals(vertice.GetDadoValor()))
                    {
                        a_clone.SetVertice1(v_clone);
                        retorno = a_clone.getVertice2;
                    }
                    else
                    {
                        a_clone.SetVertice2(v_clone);
                        retorno = a_clone.getVertice2;
                    }

                    if (!v_clone.FoiVisitado())
                    {
                        vertices.Add(v_clone);
                        v_clone.SetVisitado(true);
                    }
                    this.CloneUtil(vertices, retorno);
                };
            }
        }

        /// <summary>
        /// Informa o grau de um vértice (Número de arestas que ele possui)
        /// Retorna -1 se o parâmetro for nulo.
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public int GetGrau(Vertice v1)
        {
            if (v1 != null) return v1.GetGrau();
            return -1;
        }

        /// <summary>
        /// Verifica se um vértice possui ligação com outro
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsAdjacente(Vertice v1, Vertice v2)
        {
            if (v1 != null && v2 != null)
            {
                foreach (Aresta aresta in v1.GetArestas())
                {
                    if (aresta.GetVertices()[0] == v2 || aresta.GetVertices()[1] == v2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se todos os vértices estão conectados com todos os outros
        /// </summary>
        /// <returns></returns>
        public bool IsCompleto()
        {
            int valGrauCompleto = this.vertices.Count - 1;

            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.GetGrau() != valGrauCompleto)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifica se existe um caminho entre os vertices
        /// </summary>
        /// <returns></returns>
        public bool IsConexo()
        {
            this.LimpaVisitaVertices();
            this.componentes = this.Componentes();
            return this.componentes == 1;
        }

        /// <summary>
        /// Retorna o número de componentes do grafo
        /// </summary>
        /// <returns></returns>
        public int GetComponentes()
        {
            this.LimpaVisitaVertices();
            return this.Componentes();
        }

        private void Visitar(Vertice vertice)
        {
            vertice.AtualizarCor();
            foreach (Vertice vertice2 in vertice.GetAdjacentes())
            {
                if (vertice2.Cor == Cor.BRANCO)
                {
                    this.Visitar(vertice2);
                }
            }
            vertice.AtualizarCor();
        }

        private void Visitar(Vertice v, List<Aresta> a)
        {
            if (v != null && a != null)
            {
                List<Vertice> lstVAux;
                foreach (Aresta aAux in a)
                {
                    lstVAux = aAux.GetVertices();
                    foreach (Vertice vAux in lstVAux)
                    {
                        if ((v.Equals(vAux) && vAux.FoiVisitado()) == false) // não pode ser o vertice de origem e não pode estar visitado
                        {
                            Visitar(vAux, vAux.GetArestas());//vai para proximo vertice
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Verifica se todos os vertices tem grau par e é conexo, ou seja, euleriano
        /// </summary>
        /// <returns></returns>
        public bool IsEuleriano()
        {
            if (this.IsConexo())
            {
                foreach (Vertice vertice in this.vertices)
                {
                    if ((vertice.GetGrau() % 2) != 0) // todos vertices devem possuir grau par
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se um vértice não possui arestas
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public bool IsIsolado(Vertice v1)
        {
            if (v1 != null) return v1.GetGrau() == 0;
            return false;
        }

        /// <summary>
        /// Verifica se o grafo não possui arestas
        /// </summary>
        /// <returns></returns>
        public bool IsNulo()
        {
            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.GetGrau() > 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifica se um vértice possui grau 1
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public bool IsPendente(Vertice v1)
        {
            if (v1 != null) return v1.GetGrau() == 1;
            return false;
        }

        /// <summary>
        /// Verifica se os vértices possuem o mesmo grau
        /// </summary>
        /// <returns></returns>
        public bool IsRegular()
        {
            int grau = this.vertices[0].GetGrau();
            foreach (Vertice vertice in this.vertices)
            {
                if (grau != vertice.GetGrau())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifica se existem dois vértices de grau ímpar
        /// </summary>
        /// <returns></returns>
        public bool IsUnicursal()
        {
            int countImpares = 0;
            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.GetGrau() % 2 != 0)
                {
                    countImpares++;
                    if (countImpares > 1) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Define a cor de todos os vértices do grafo como BRANCO.
        /// </summary>
        private void ResetarCorDosVertices()
        {
            this.vertices.ForEach(vertice => vertice.ResetarCor());
        }

        /// <summary>
        /// Converte todas as informações do grafo em um string no seguinte formado
        /// <para>
        /// Vertice VERTICE
        /// <para> VERTICE_1 - VERTICE_2 ; Peso PESO_ARESTA</para>
        /// </para>
        /// (Essa estrutura é repetida para cada vértice do grafo.)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder retorno = new StringBuilder();
            foreach (Vertice vertice in this.vertices)
            {
                retorno.AppendLine("Vértice " + vertice.GetDadoValor());
                retorno.AppendLine(vertice.ToStringComArestas());
                retorno.AppendLine();
            }
            return retorno.ToString();
        }

        public string ToStringSimplesSemEspaco()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Vertice vertice in this.vertices)
            {
                builder.Append(vertice.ToStringComArestasSemEspaco());
            }
            return builder.ToString();
        }

        /// <summary>
        /// Verifica se o arquivo está no formato para ser um grafo, 
        /// Se houver qualquer erro na verificação, é retornado false
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileAGrafo(string[] file)
        {
            try
            {
                return file[1].Split(';').Length == 3;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>	
        /// Depth First Search	
        /// </summary>	
        /// <returns></returns>	
        public int Componentes()
        {
            int componentes = 0;
            this.ResetarCorDosVertices();

            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.Cor == Cor.BRANCO)
                {
                    this.Visitar(vertice);
                    componentes++;
                }
            }
            return componentes;
        }

        /// <summary>
        /// Torna a visita das arestas de cada vértice como FALSE
        /// </summary>
        private void ResetarVisitaArestas()
        {
            this.vertices.ForEach(vertice => vertice.ResetarVisitaArestas());
        }

        /// <summary>
        /// Adiciona todas as arestas dos vértices na lista de arestas
        /// </summary>
        private void BuscarArestasNosVertices()
        {
            this.vertices.ForEach(vertice =>
            {
                vertice.GetArestas().ForEach(aresta =>
                {
                    if (!aresta.FoiVisitado())
                    {
                        this.arestas.Add(aresta);
                    }
                });
            });
            this.ResetarVisitaArestas();
        }

        /// <summary>
        /// Adiciona a lista de vértices e aresta no grafo, verificando se, na lista
        /// de arestas, elas não estão inseridas nos vértices ao qual estão ligadas. 
        /// Se não estiverem, são inseridas nos respectivos vértices
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="arestas"></param>
        private void OrganizarVerticesEArestas(List<Vertice> vertices, List<Aresta> arestas)
        {
            Vertice v1, v2;

            if (this.vertices != null)
            {
                vertices.ForEach(vertice =>
                {
                    if (!this.vertices.Contains(vertice)) this.vertices.Add(vertice);
                });

                arestas.ForEach(aresta =>
                {
                    v1 = aresta.getVertice1;
                    v2 = aresta.getVertice2;

                    if (this.vertices.Contains(v1) && this.vertices.Contains(v2))
                    {
                        if (!v1.Contem(aresta)) v1.AddAresta(aresta);
                        if (!v2.Contem(aresta)) v2.AddAresta(aresta);
                        this.arestas.Add(aresta);
                    }
                });
            }
        }

        /// <summary>
        /// Retorna uma lista com o valor do dado de cada vértice
        /// </summary>
        /// <returns></returns>
        public string[] GetValoresVertices()
        {
            List<string> retorno = new List<string>();
            this.vertices.ForEach(vertice => retorno.Add(vertice.GetDadoValor().ToString()));
            return retorno.ToArray();
        }

        public string Json()
        {
           return new JavaScriptSerializer().Serialize(this).ToString();
        }
    }
}
