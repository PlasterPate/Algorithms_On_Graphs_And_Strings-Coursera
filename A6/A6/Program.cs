﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "GCGAAATAA$";
            string bwt = BWT.BWTransform(input);
            Console.WriteLine("Bwt is :  " + bwt);
            string invertBwt = BWT.InverseBWT(bwt);
            Console.WriteLine("Primary text is:  " + invertBwt);
            //if (input == invertBwt)
            //    Console.WriteLine("yeeeeeeeeeeees!!!");
            //else
            //    Console.WriteLine("riiiiiiiiiiiiiiiiiid");
            Console.WriteLine(BWT.InverseBWT("CTAGTAACTCGTGCCTCTTCGCGATGTGCCTCGACTTCCTTGCGTACGGGATCGCCACTCATGGTCCGGCCCGGACTGTGGAATATAGAGGTAACCCACACCAGAAAGGCTTTCCCCACTAGGAATCAAAGTAGTGCCGGCTTCGCTCGACCCGAAGACCGCCTCTTCTCTTCCTCACCATTTGTCCTGTCAGGGCGCCCCTGCCGCTACCTCGGTAAAGCTGAGTCTGCAGTGTGAAATGATTCGGCTCCTCGGGTCCCACAACACAGACCATAGGCTGGATCCACTAGACGCCGTGTCCACGATGTGATTTATTTTGAAAGTTGGACCAACGCGCCAGAGCTTCTCTGCGCACGGTATACACCCTGTCCTGTTTCGATCGTTGTGTAGCCAGTTGTTCAACCTGAGAGTGGCACGCGAGAAAAGCAGCCCCGCGCAATACTTAGCTGTTTCGTGCAATGCGAAACTAGACCAATAATTGAGTGGGTGTGATCCCGAGTCGTATCTTGACCAAGCGCTCGTATAGCACAGGAGGTACCTTCGCAAGGCTCTCCAGGGGCCATAAAAAGCAGTGGAAATGGGCGTACCTTCCCAGGGCCGCAATTGAGTCTCTTCTGGTGCACGAAATACGCGTCAACCCGCCCACCCTTCCTCGGACCAATTCCAGCATTGTGCAGTCTTACACACGTAACGTTGTACTCAGGCCCGTCCAATGCGCTATTCAACTCCCTGAAACTAGCACTTTCCGGGGAAGGCGGACTGCAGGCTGATACATTGAGCTACAGGTGAGTCCGTTGCATATTTGGGTGACCTGCGTAGAGCGTTGAATCGGGATGTGGACTCGGAGGTTTGAGCTTCGTGGTGGCTCCCTGGACGAACGCAGATCTTTTGTGCTCCGATTTACTAGGGCGGGCCGGTCTAAGGATCTCTTTCGCTGCGGGATGCACGCACGACGATTATATCGATTGCCGCCTGACGTCAATATGCCCTATTACGTTCCTCCCTAGCAGGAATAAGTTGAAGAATTAAGATACCAATGTGGGGCTTCAAGAAGAACCTTTTTGGGTCCCACGAAAAAAAGACGGTTGCCCGAGATTGTCAGCCCGGTGTATTGCAGGTAAGAGTATGGTACTTTTATCTTCCATGCGAAACTTCACACATCATTGCGCACTCAGCGCTGATGGACAACGGACGGCAGACAACTGATTCGGCGGAATTTATCAGCAGCTGGAGCATTGGGCAGAGAGACTTGCCCAACGGGGTAAACCTTCTGCATGTTAGGAAGTTGTAGGTCTGCCGAACGTTTCAAGCCGAGGGAATAGGAACTACTGTAAACAGGTCCATCAAGGATCCTACCCCGGGGGTCAAATACCCCAGCCGCATAAGGCGATTCTAGAAATACCGTGGTTGTCATTCATGGTCTGTCATGGCCTATGTTTTTCAAAGTGGAGGGACACCTAGGTGCTTCTGCATCCGGAATACATGTGTACGCTGCACTCCTATCTTTAACACCCATGCTAGTATACATACATCCTTCTTTAACCCTAAGGTGACTTATCTTTCTCTGAACGGTACCCAGTGGTCACGTAGGTGGGCCCATCTTTCCAATTAGGCCTCACGACCCGATGGTGGGGAGTCTGATCCCGATGCAGGACCCCAGGTCCTTCACCGCCATCTCGAAGTGACGTCGCGATCCCGTCAAGATTAGAAACTCCAGTTGGGGTGTCGTCTCCAGCGGTCCGCAAGAGGTTCTACCCACCCTGATCTACTACAAGACGGGGTAATTATGTTGACTATTCCAACCGTGACTCTTGTGGAATAGGTTTACTAAGCCTCTAGAGGCGACTTCATTTCCACAGAGATAGTTACCGTTATGGATTTTGGTGGCCAGCGGTCCTTCCTAGATTGCTTTACATCATGGTGACACTCGGGTACGGAGGATAATTATCGTTTTAGGTCTGAGTATTCAAATGCCTGATAAAGCGCTTTGCAGACTCGTCTTTGCAGCAGCTTATAGATCATGATTGACACGACCGACTAGATTTCGAGTGCAGAAGCGACTAATAGCACCAACCGTCTGACAACTACTTCAGGGGGAGCTCCAAGTCCAGCCTAACTGGTGAAAGAATGTGAGATCCTATGACCCGTATCGT$CCTAATACAATCTGGACGCCTTAAACCAATCATCCCGGACAAGTTTCTGTTCTACAAAATTAGTGTAAAGCCAGGTGTGCTTCGCTTTCTTAAGGTAGACGGACTACTATCTAATCGTCAGTCGGGGCGTGGTAACACAGACCTAGTCCCTTGCACCCAAATTCCTCAGACTAAGTTATTCGGTTCTCTGAAGCAGTGGATGCTATAACTCCAGAAGATGGTTTGGAGTGTGGAGGTATCGGCTCACAATTGGGCGCCAGGCGGTGAATTTAGTCACTTCGGTTCCTATGCCATAAAGCTGACAGTGAGGAAGCGAAAGGGCGCCACATCAGTGGACGTCGCACTGACCTGGGCCGAATTAGCTACTTTGTTAATTAGACGGGGATTTCCACGACTTACCAGGCGCTCATTGCAACAGAGAAGCGGGCAGAAGTCCTCAAATTGTACCTTCTCTCAATATTCCAGTCTACGGTTGTGGAGCCGTCGGGGAGTACCACGCAGGACTTTTAAGGAAGCTAAGCGTCGCACCGTGGCATTAGTGTATAGGCAACGAACCTCATCGTAGTAGGGGACATAAGGTTTCGCTATTTGGCATTTAATACTACAAGGACCGGAAAGGTTTAGCCGTTTCATGTCCCATTAAATCAGGACAGACAGCGAGTAATTAATGCGCGCTGAGAGCGTCGTGGCGAGGGGATGAGGGGCGCACGCTGACCCTCGACCGTGGCGCTTTTAGCCTAATACTCTGAAGGACCGGACACGTGCTTGGCGGGTCCAAACAATCAATCTCGGTGTTCACGCGAAGAGCGCAGCAATGGACGTCGATGGATTGCAAACTGATAGCAGTTCGCTAATCTAACACGAGTCGGTGAAGCAGTATACCTGGGCACCGGTGCTCGGGCTATCGGAAGAAGACATGTGTTCTCGCGGGGAAATGTTCGGACCATATCTAGCTCTGAATGCATCCGACAGAATCAACTTTCCTGTGTGTGCACTGAACCCGACGCATGTACAACCTAATTCAAGCACCCGGGACCTGCTAAGCTGATCAACCCGCTAGGTAAAGATATGTCCAGCGAGATTGACATTCCACGAACCCGAAGTGCAGTGCTTCTTTTTCGAACCTATGTGTGAAACTCGTGAGTGACACGTGCGTAAAAAAAATTTCCTCATAACGATCGACCCGTTGCAGATAACGACCGAACAAATTGGCCGCGACGTGCCCACCCACCCTACGGACCTTGCGTTGCGAACGATCTTAACCGGGATATGTGTCTCTTGAGGGGCAGAACACCCGCGATTCTCGTCGGGCAAGTCGTACAAATGAATGCAGTTGGCCAGTCACTGCTTCGATTCTCCGGGTGTAGATGAACGTACTGTGCTCGCGCCCCGGATTCCCCTTAAAGCTTAAGGTCAAGGATAGAGGTGGGGTGGCCGTTTACTATGACCCCCTCGCTCGCGAGTATGAACCTTGTCCCAACATTAAAATATGATACATTAGAGGCGGGAGGTCTTCGGCTACGCCTTCCATAGGGGTTTCAAAACCCGTACACCTACACGGTGCCGACTCCACTAACGAACCCCCAAGGAAAGTCAATCTAGGCCGTCTCCCGGGTTAGTGTCCCTCCTTCACCCTCGTAAGAACGCACCGATGACGTCTCGTCTTGATCATCCGCCGTAGGTGCCTTGTCGGGGTCCGGTATCATCCACACTCCTCCATCTTGTCTGAGGTTGAGAGTCCTAACGTTGGGCTACCTTCCCCATGAAGAAGTTCAGCAGATCTTGATCGAGCTTACTGAGATTGTAGCTTGCCTTGAAATATTATCAATACAATAAGTGTTGATGCCACTCTATCCCGGGGCATCTTCTTGTGCTGGCAAAAAATTGGCTCACTCGCGTACACTTCCGGCTATACGTGCAACGCCGCACGTTCTAGTTATAAGTAACCCTCTACAAGGGAGGAGGCTAAACCGTAGTCCGTACGTAAATTGCAGGGTCCAGGCGCTGGGGGTTATGGGGACACACGTTTGACAGATCTAGAACAGCCCGTATCTATCTCCCCCCTTCTCACTCGACACCGTTATAGGACTTTGTTTCGCTGGTATTCTCTAGACATAATTGGTTCTCCACAATGACTCCTGGCACCGAGGCCACCGTAAGCTATATGTCCGCGGTAAAAGGGTTCATCGAATGAAAAGGTGCAGACGAAGATGTGGCGCGCGACTGCGTCCTTGATCATGACAGGGTCGTGTCTTCAATGGACACTATGTGCATTTCCTCCGCGCCCGCGACCACCTGCGACCGCCAGAGCCGGCACAGCATCTAAGCGTGTAATTAGTACCCAGAGCATACGTCTTGTTGCGGCATAACAACAGTGGCTGTGCTTGGGCGAATCAGCAGGTTCGGCGGTTGCACGAACCCTGCGTCGCACAGGAATGTCATGCAGAGGTTGGGTGATATCTCTGTTGACGTATGGATGCCAAATTGTCGGCAGTTCCAGTAACATACGCACTTAGCCCTCAAGGGTACGACGGTTGCCGAGAGTGCTGCGTCCCCAAGTTGAAACGACGCCCGAAGTCATTTTTTTCGGCTCTCCAGGTTAGTCGAAGGTGTCTACTATCCGTTGGGGCAAGACGTCCAGCAAGCTCAGGTATCTTCCAGCTCTGCGTTCATACATGCTGTGAATCTCTTAGGTTCCCCCTACATTTCGCGACGCACCGCTTCCTGACCTCCTCCTGAATTCCGCAAGTCTCAAAACCTCCTCCTAATCAATACCACGCCTGCCACTTTATTCAGCGTTGATTACGTCTACTCGCATATACTTGGCTAAGACAAGGGGTGGGGAGGATGGGGTTTCCGATGGATAATTAAAATTGCGGCTACGTGGTACTCCATGCGCTGAAAGTGAACACCCACTACTCTCACATATTGCAATGGTTGGATTGGCATACGGCTCAGTTCGGTATATTTTCGGTGTGAATGGTCGTGGTAGGTCTAGCAGATTGGTTCCTACACCCCATGGAGCTGCTCACGTGTGCACATACGATACGCGTAAGTTGAGGAAAAGTAGATAGCATCCGGGCAACTTGATCGCGAACCTGCGGATGATTGGGCCAAGTCCCCGTCAACTGCGAAGTTCGAACGATCCATCTTCCGGGCAATCTACTACACCTAGGCCATGGAATTAAACCATGCACATCCGAGGGCCAGCGACTCTATCGCCCCCAATGTTGGCCGGTAGGTCAACCGGTTTTTCGCGTAAACAGGACATGTCAATTTACTTTGATTAGAGTGCCCTATAACGACGATTCGTTGGTATTCTATCTCGGCGGAAACGCGGACAACAGTTTAACCGTTTTTGTATATAGGGTGAACATATATCATGGTAGTCCACGATGTCTGTTGCAGAGCTGCTGTACCATAACGACACACATATCGGAACCAGGGCGCATGCGGGGACCTAAGGGGGTCCAGGTGTCAGGCAAACACGAACCGGTTCATTCGTATGTACAATTCCTTCGGTGTAAGAAAAAGCAGGACGTCCCTATGCTGTTGCAAAAGAATGTTACTCCTTTTCCTCTTGTAGGTTAGCCGATTGGCGCGTCGGCGCGATGATCGGTTAATTTAAGCTCTATACTCCCTCAAGGACGTTTCTCGACCTCGGCCCCTACATACGAACGACTATAGAATTCACATGTGGAACGGTCAATACCTTTCTGGTATTTCAAGGGGATTTGGTGGCCTTGGGCATCAGTCCGACTCGTCTAGACTATTAGGTCGAATATATACTGTCCACTCTAGGTGTTCCGAGAGGGTATCTTAAAAGCTGGCCTTCCATCACTAGAAATGTGAGTCAAGTTACCTAACTGTCATGGGCAGGGAATCTCTGAGAGTCATATCGACGCACTTGTCGGATTGGACGAGCGTGGATCCTCGGCACCGTCTTTCCTAAGCTTGTGTGAGGACGCCCCATGCCACTGTCCAGATTTGCACACAACGGCCGGGCGCATATACTCCGAAGGATCAGCGCGAAGGATAGGTAGTATCGCAAGGAGGACATGCTTTGACTGCTTAAGGTCCGCATTGGCACCTACTGGATTAGAGTAATACGTCGAGATGCGTCGGGACATTTTCTCGCTTCCAGCGGCTTATCCGGAGGCGTCACAATTGTCCGAGATTGCGAAATATGCAGACTGTAACTAGCTCCCTGTGTGGTTTTGCGAGTACGAAGAAGGGCTGCCGTATAATGAGAGGGCTTACGCCAAACAGCGGCGCCCAACAGCCAGGCGGAAGGCGCCACCTATATTACTAAGCCAACTCTTCTTGCTTATTCTGAGCCGGACGATTCCCGGACGACTTCATAACTTATCCTACGTTGAGCGCAGCGTGTTGACTAATCGCATTACCACGGCGGTTTGGCTGGCGCCCCATCTCTGTAACCATGTGGTTATACGAGTCTGGGCAATACGGACCCTGGTGTATGCCCTTGTAGTTGTTCTACATTAGCGTGTTCGTACCTGCTTGGGCTCAGCCCCGCGCCCCAGAGAATATAGTTTATTCTGTCAGCGGACCCCTGGGCTGAAGCTCAATTACTGTTCCATGCTGGAGTGACTCTCAAAGTGCCATGGATGCAGCGACGAGCCTGCCCCTTTAAAAAGCTGTACATTGGAACTGCTTTTCTAGACCCCAGCTCACCTTAGCGTATAGGGCTTCTGATAGGACGCTCGAATGTATGTCAACGTATCGCGCTATCTATAGTCCAGACGACTAGCGTTGGGGGGAACTCTACTCGTACGCGCCGCCGAACTTACCCTTTATCAGTAAGTCTTCTACGCTCTATCCTTTGAGTTCCCCCTGCCTCATTGACGTCATGAGGGCCTAGAAGCTGCCTGATAATGGTCGCGCAACGTGTATTCCCCGGCCAGATTAGACAATACGTGCACGACATTCCTCCTCGACCCTTCTGCCGGCGCGAATGGCCACGAAAACTTCTGTGCCGTTCTGGCTTTATGCAACGGAATCCAACACCATAGTCATCCCAACGCCAACCCGACGCTAGGCTCGCGGGTCACCGCAGGATAGTACTCCCCTGGATGCCACTGATCTAGACCTAAGGAGGCGTATACCGACTACCCAAGAGATAGAATAGCCTGTCCAATTGCCTTAACACGTCTCATGATCCGTTCCGTACTATATCTCACCTTCTAGTGATTAGTAGCCACTCGGTCGGTGTGATGGGCTGTATGTGCTTTGGTTACCTGACCTAAATTTTGAACTTTATGTTTTGACCACTAAGGCGATCGCGGAGGGGATGACGGTGCCCGTTCCATCACCCAGAACATGGGCTTGGGTTGGTGACAAGCGCAACATAACTCATGCGGACGTGGTAACGCCACTTCTCTATCTCAGCGGAGGTCTCGGATGCACAACGAGCCGGCGAGTAAGGCCGCCTACTTTAGGGGCAGACCTCCCTGCTTCAATCGAAATGCGGCTCTGGTTGATTACTCGACGTACTTAAGCGGCCATATGGGTACAAATCACCCACACCCATGCGACGGACTTATGTAAGCTCTCAACTTTAACCACTTTGACCATAATTGAGGTCGGTCAAAGTCAGTCTCAGAAGTTAGTACGAGGTTACGGCGTGGTACTTCCTCGAACGTTATTTACTGAGACATCACTCGGGGGGTCACCTCACACTTCTGAACAATATAGGCGAAACCTTGCATGCCCCGATTAATATGTGTGACCGTGATTAATTTCTACACGCCTCACGTATCGGCCTTTATAACTTGGGGGGACAGCACAAGCAACAACACGTAAGTTGCGAGGGTCGGCCTCTGGTGAGTTATACATTTAAGCGTAGAGTCTATTATTTATGATAGGGTACAACTCTCCACGAGCGAACTCTATTGAGTCTCAAGCCTTACTGATTGCGTCTTACTAGGAACCAGGGCGGAGTATCAGGTGGACCCTGTGTCGCAATCACTCCAACGAGCGAATATATGTCGAACCTCAAGCCCTACTGAGACTCTTGTGTAACGTCTCCGTACAGGAAACCTGCTATTTGGTTTAATCCAATGGTGGTCGGGCTCCGTTGAGCGAGCGAGTATACCAGAAGCTACTATCGGTCTCATCCGACTCGCGGCTATCGAGGTCTCGGAACTGCAGTGAGGGTTTTCAGGAAAGCAACCGGAAGCCTTGCGGAATGCGGCGGCTGTCCCAGCCCCTTTATGTTGGCAACTTCACTCTAAGCTATATTTGTCCCCACCCAATCGCAGCAACCTTGCTGTCCATCCGTGGCCGACGCGATTTCCAAGTCATTTTATGGGGTGACACTTTCGATGTGTTGTTCGCAAGACCAGCACCGGCAAAATAGGGGTCGAGCGTCGCTGATGTCAGCTCTTCGGGCAACATGTATAAGTGCTCGAAATCTCTCAGAACTCGTCGAGGTCGAGATAATCTGTATTACAATCAGAAAAGGGGTAACATCCAAGACGGTACAGAGCGAGTTACGCAGGGTAAACAAGACCGAAGGGCGCGCGGGCTTTAAGCTGGGACCCTTTCGGTTTTGTGGCCCGAATATATGAGTACATTCTTGAAATTGATAAATGGGCCGAAATCCACCGGCGGTGGTGGCGTTCGCTCGTAAGACTGGTCGATGGTATCTATACTACAGGGTTCGGTAACCCCACAGATGGGGTCTTGCCAATCCAATAGACGTTCTCAGTGCGTTAAAGACGACCCTAGTCTCTGATGGAGATGTGTCTTTCCCTGGTCTGGAGAAGTCGGAGTCTCAAAATTCTATATGCATGCATGTGCAAGATCTACGGAGCTTTGCCTCGTCGGCTCGCTCCTTGAGTTAAGTATCACCCGTTCCGCATGCATAACAAGCATACCTCGCGTCCACTTGGCCCGTTCCATTGCACGCCGCAGAGGAAAGGTTTCCTATGTCTTACTGCCGACCCACCAGTTCATACACGCGCACTCACGTCTCATTTCGGTCGCGGACCGCCTATTCACGGACCCGGATTTGAGCTCCCGATCCCCACCACATCCGCTCATAAGGTCCAATCCATCCCCAGATTGGGAACGGCGAAGCCCCCCAGTTGTCGGGCCAATGTGGAAGAGATCTACGAATTTGCGCATCGGGCGATTATCACACGTGACAGCAGAAGCGTCTAGTTGTTTTCGACTCGTAGCCACCACCGTACCATTCGGTGATTAGCCTCAAAAACTAGGGGGTCTTAGTTATGGTGCGCCAGATTCTTAAAATTTACGTTGGCTTGATTGTGCGAAGCACAACTGAGGAAACTGGACAGGGAAAGCGTGCAGATGATCGGGGACGCCGTTGCGGTCGATGCAAGAGATCGCCCTCTGCGAGGGGCAAGATCTAAGGTTGGCAGAACGGAGCCCTCAGGTGTGGTCGTTGGTCC"));
        }
    }
}